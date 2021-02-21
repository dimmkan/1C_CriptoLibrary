using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;

namespace Crypto1CLib
{
    public interface ASignatures
    {

        string packingBinaryData(string sourceString, string INN);

        string getCertificateSerial(string INN);

        string encode64(string str);



    }

    [Guid("AB634001-F13D-11d0-A459-004095E1DAEA")]// стандартный GUID для IInitDone ссылка http://soaron.fromru.com/vkhints.htm
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInitDone
    {
        /// <summary>
        /// Инициализация компонента
        /// </summary>
        /// <param name="connection">reference to IDispatch</param>
        void Init(
          [MarshalAs(UnmanagedType.IDispatch)]
    object connection);

        /// <summary>
        /// Вызывается перед уничтожением компонента
        /// </summary>
        void Done();

        /// <summary>
        /// Возвращается инициализационная информация
        /// </summary>
        /// <param name="info">Component information</param>
        void GetInfo(
          [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)]
    ref object[] info);
    }

    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class CryptoClass : ASignatures, IInitDone
    {

        //private IAsyncEvent asyncEvent = null;
        //private IStatusLine statusLine = null;

        /// <summary>
        /// Инициализация компонента
        /// </summary>
        /// <param name="connection">reference to IDispatch</param>
        public void Init(
          [MarshalAs(UnmanagedType.IDispatch)]
  object connection)
        {
            //asyncEvent = (IAsyncEvent)connection;
            //statusLine = (IStatusLine)connection;
        }

        /// <summary>
        /// Возвращается информация о компоненте
        /// </summary>
        /// <param name="info">Component information</param>
        public void GetInfo(
          [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)]
  ref object[] info)
        {
            info[0] = 2000;
        }

        public void Done()
        {
        }

        public string packingBinaryData(string sourceString, string certSerialNumber)
        {
            byte[] sourceData = Convert.FromBase64String(sourceString);
            X509Certificate2[] certArr = new X509Certificate2[1];
            using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);
                foreach (X509Certificate2 cert in store.Certificates)
                {
                    if (cert.SerialNumber == certSerialNumber) {
                        certArr[0] = cert;
                    }
                }
            }

            CmsSigner cmsSign = new CmsSigner(certArr[0]);

            ContentInfo contInf = new ContentInfo(sourceData);
            SignedCms signCMS = new SignedCms(contInf);
            signCMS.ComputeSignature(cmsSign);

            byte[] encodedData = signCMS.Encode();
            return Convert.ToBase64String(encodedData);
        }

        public string getCertificateSerial(string INN)
        {
            using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);

                foreach (X509Certificate2 cert in store.Certificates)
                {
                    if (cert.Subject.IndexOf(INN) != -1)
                    {
                        return cert.SerialNumber;
                    }
                }
            }

            return "";
        }

        public string encode64(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

    }
}
