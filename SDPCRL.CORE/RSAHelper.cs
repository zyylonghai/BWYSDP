using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SDPCRL.CORE
{
    ///// <summary>
    ///// RSA加解密帮助类
    ///// </summary>
    //public class RSAHelper
    //{
    //    #region 构造函数
    //    public RSAHelper()
    //    {

    //    }
    //    #endregion

    //    #region 私有函数
    //    ///// <summary>
    //    ///// 生成密钥对(2048位)
    //    ///// </summary>
    //    ///// <returns>返回密钥对数组，数组下标0为私钥，数组下标1为公钥</returns>
    //    //private  string[] GenerateKeys()
    //    //{
    //    //    string[] sKeys = new String[2];
    //    //    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
    //    //    sKeys[0] = rsa.ToXmlString(true);
    //    //    sKeys[1] = rsa.ToXmlString(false);
    //    //    return sKeys;
    //    //}

    //    //private static RSACryptoServiceProvider DecodePemPrivateKey(String pemstr)
    //    //{
    //    //    byte[] pkcs8privatekey;
    //    //    pkcs8privatekey = Convert.FromBase64String(pemstr);
    //    //    if (pkcs8privatekey != null)
    //    //    {
    //    //        RSACryptoServiceProvider rsa = DecodePrivateKeyInfo(pkcs8privatekey);
    //    //        return rsa;
    //    //    }
    //    //    else
    //    //        return null;
    //    //}

    //    //private static RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] pkcs8)
    //    //{
    //    //    byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
    //    //    byte[] seq = new byte[15];

    //    //    MemoryStream mem = new MemoryStream(pkcs8);
    //    //    int lenstream = (int)mem.Length;
    //    //    BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading    
    //    //    byte bt = 0;
    //    //    ushort twobytes = 0;

    //    //    try
    //    //    {
    //    //        twobytes = binr.ReadUInt16();
    //    //        if (twobytes == 0x8130)    //data read as little endian order (actual data order for Sequence is 30 81)    
    //    //            binr.ReadByte();    //advance 1 byte    
    //    //        else if (twobytes == 0x8230)
    //    //            binr.ReadInt16();    //advance 2 bytes    
    //    //        else
    //    //            return null;

    //    //        bt = binr.ReadByte();
    //    //        if (bt != 0x02)
    //    //            return null;

    //    //        twobytes = binr.ReadUInt16();

    //    //        if (twobytes != 0x0001)
    //    //            return null;

    //    //        seq = binr.ReadBytes(15);        //read the Sequence OID    
    //    //        if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct    
    //    //            return null;

    //    //        bt = binr.ReadByte();
    //    //        if (bt != 0x04)    //expect an Octet string    
    //    //            return null;

    //    //        bt = binr.ReadByte();        //read next byte, or next 2 bytes is  0x81 or 0x82; otherwise bt is the byte count    
    //    //        if (bt == 0x81)
    //    //            binr.ReadByte();
    //    //        else
    //    //            if (bt == 0x82)
    //    //            binr.ReadUInt16();
    //    //        //------ at this stage, the remaining sequence should be the RSA private key    

    //    //        byte[] rsaprivkey = binr.ReadBytes((int)(lenstream - mem.Position));
    //    //        RSACryptoServiceProvider rsacsp = DecodeRSAPrivateKey(rsaprivkey);
    //    //        return rsacsp;
    //    //    }

    //    //    catch (Exception)
    //    //    {
    //    //        return null;
    //    //    }

    //    //    finally { binr.Close(); }

    //    //}

    //    //private static bool CompareBytearrays(byte[] a, byte[] b)
    //    //{
    //    //    if (a.Length != b.Length)
    //    //        return false;
    //    //    int i = 0;
    //    //    foreach (byte c in a)
    //    //    {
    //    //        if (c != b[i])
    //    //            return false;
    //    //        i++;
    //    //    }
    //    //    return true;
    //    //}
    //    //private static RSACryptoServiceProvider DecodeRSAPublicKey(byte[] publickey)
    //    //{
    //    //    // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"    
    //    //    byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
    //    //    byte[] seq = new byte[15];
    //    //    // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------    
    //    //    MemoryStream mem = new MemoryStream(publickey);
    //    //    BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading    
    //    //    byte bt = 0;
    //    //    ushort twobytes = 0;

    //    //    try
    //    //    {

    //    //        twobytes = binr.ReadUInt16();
    //    //        if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)    
    //    //            binr.ReadByte();    //advance 1 byte    
    //    //        else if (twobytes == 0x8230)
    //    //            binr.ReadInt16();   //advance 2 bytes    
    //    //        else
    //    //            return null;

    //    //        seq = binr.ReadBytes(15);       //read the Sequence OID    
    //    //        if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct    
    //    //            return null;

    //    //        twobytes = binr.ReadUInt16();
    //    //        if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)    
    //    //            binr.ReadByte();    //advance 1 byte    
    //    //        else if (twobytes == 0x8203)
    //    //            binr.ReadInt16();   //advance 2 bytes    
    //    //        else
    //    //            return null;

    //    //        bt = binr.ReadByte();
    //    //        if (bt != 0x00)     //expect null byte next    
    //    //            return null;

    //    //        twobytes = binr.ReadUInt16();
    //    //        if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)    
    //    //            binr.ReadByte();    //advance 1 byte    
    //    //        else if (twobytes == 0x8230)
    //    //            binr.ReadInt16();   //advance 2 bytes    
    //    //        else
    //    //            return null;

    //    //        twobytes = binr.ReadUInt16();
    //    //        byte lowbyte = 0x00;
    //    //        byte highbyte = 0x00;

    //    //        if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)    
    //    //            lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus    
    //    //        else if (twobytes == 0x8202)
    //    //        {
    //    //            highbyte = binr.ReadByte(); //advance 2 bytes    
    //    //            lowbyte = binr.ReadByte();
    //    //        }
    //    //        else
    //    //            return null;
    //    //        byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order    
    //    //        int modsize = BitConverter.ToInt32(modint, 0);

    //    //        byte firstbyte = binr.ReadByte();
    //    //        binr.BaseStream.Seek(-1, SeekOrigin.Current);

    //    //        if (firstbyte == 0x00)
    //    //        {   //if first byte (highest order) of modulus is zero, don't include it    
    //    //            binr.ReadByte();    //skip this null byte    
    //    //            modsize -= 1;   //reduce modulus buffer size by 1    
    //    //        }

    //    //        byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes    

    //    //        if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data    
    //    //            return null;
    //    //        int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)    
    //    //        byte[] exponent = binr.ReadBytes(expbytes);

    //    //        // ------- create RSACryptoServiceProvider instance and initialize with public key -----    
    //    //        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
    //    //        RSAParameters RSAKeyInfo = new RSAParameters();
    //    //        RSAKeyInfo.Modulus = modulus;
    //    //        RSAKeyInfo.Exponent = exponent;
    //    //        RSA.ImportParameters(RSAKeyInfo);
    //    //        return RSA;
    //    //    }
    //    //    catch (Exception)
    //    //    {
    //    //        return null;
    //    //    }

    //    //    finally { binr.Close(); }

    //    //}

    //    //private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
    //    //{
    //    //    byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

    //    //    // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------    
    //    //    MemoryStream mem = new MemoryStream(privkey);
    //    //    BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading    
    //    //    byte bt = 0;
    //    //    ushort twobytes = 0;
    //    //    int elems = 0;
    //    //    try
    //    //    {
    //    //        twobytes = binr.ReadUInt16();
    //    //        if (twobytes == 0x8130)    //data read as little endian order (actual data order for Sequence is 30 81)    
    //    //            binr.ReadByte();    //advance 1 byte    
    //    //        else if (twobytes == 0x8230)
    //    //            binr.ReadInt16();    //advance 2 bytes    
    //    //        else
    //    //            return null;

    //    //        twobytes = binr.ReadUInt16();
    //    //        if (twobytes != 0x0102)    //version number    
    //    //            return null;
    //    //        bt = binr.ReadByte();
    //    //        if (bt != 0x00)
    //    //            return null;


    //    //        //------  all private key components are Integer sequences ----    
    //    //        elems = GetIntegerSize(binr);
    //    //        MODULUS = binr.ReadBytes(elems);

    //    //        elems = GetIntegerSize(binr);
    //    //        E = binr.ReadBytes(elems);

    //    //        elems = GetIntegerSize(binr);
    //    //        D = binr.ReadBytes(elems);

    //    //        elems = GetIntegerSize(binr);
    //    //        P = binr.ReadBytes(elems);

    //    //        elems = GetIntegerSize(binr);
    //    //        Q = binr.ReadBytes(elems);

    //    //        elems = GetIntegerSize(binr);
    //    //        DP = binr.ReadBytes(elems);

    //    //        elems = GetIntegerSize(binr);
    //    //        DQ = binr.ReadBytes(elems);

    //    //        elems = GetIntegerSize(binr);
    //    //        IQ = binr.ReadBytes(elems);

    //    //        // ------- create RSACryptoServiceProvider instance and initialize with public key -----    
    //    //        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
    //    //        RSAParameters RSAparams = new RSAParameters();
    //    //        RSAparams.Modulus = MODULUS;
    //    //        RSAparams.Exponent = E;
    //    //        RSAparams.D = D;
    //    //        RSAparams.P = P;
    //    //        RSAparams.Q = Q;
    //    //        RSAparams.DP = DP;
    //    //        RSAparams.DQ = DQ;
    //    //        RSAparams.InverseQ = IQ;
    //    //        RSA.ImportParameters(RSAparams);
    //    //        return RSA;
    //    //    }
    //    //    catch (Exception)
    //    //    {
    //    //        return null;
    //    //    }
    //    //    finally { binr.Close(); }
    //    //}

    //    //private static int GetIntegerSize(BinaryReader binr)
    //    //{
    //    //    byte bt = 0;
    //    //    byte lowbyte = 0x00;
    //    //    byte highbyte = 0x00;
    //    //    int count = 0;
    //    //    bt = binr.ReadByte();
    //    //    if (bt != 0x02)        //expect integer    
    //    //        return 0;
    //    //    bt = binr.ReadByte();

    //    //    if (bt == 0x81)
    //    //        count = binr.ReadByte();    // data size in next byte    
    //    //    else
    //    //        if (bt == 0x82)
    //    //    {
    //    //        highbyte = binr.ReadByte();    // data size in next 2 bytes    
    //    //        lowbyte = binr.ReadByte();
    //    //        byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
    //    //        count = BitConverter.ToInt32(modint, 0);
    //    //    }
    //    //    else
    //    //    {
    //    //        count = bt;        // we already have the data size    
    //    //    }



    //    //    while (binr.ReadByte() == 0x00)
    //    //    {    //remove high order zeros in data    
    //    //        count -= 1;
    //    //    }
    //    //    binr.BaseStream.Seek(-1, SeekOrigin.Current);        //last ReadByte wasn't a removed zero, so back up a byte    
    //    //    return count;
    //    //}

    //    //private static RSAParameters ConvertFromPublicKey(string pemFileConent)
    //    //{

    //    //    if (string.IsNullOrEmpty(pemFileConent))
    //    //    {
    //    //        throw new ArgumentNullException("pemFileConent", "This arg cann't be empty.");
    //    //    }
    //    //    pemFileConent = pemFileConent.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "").Replace("\n", "").Replace("\r", "");
    //    //    byte[] keyData = Convert.FromBase64String(pemFileConent);
    //    //    bool keySize1024 = (keyData.Length == 162);
    //    //    bool keySize2048 = (keyData.Length == 294);
    //    //    if (!(keySize1024 || keySize2048))
    //    //    {
    //    //        throw new ArgumentException("pem file content is incorrect, Only support the key size is 1024 or 2048");
    //    //    }
    //    //    byte[] pemModulus = (keySize1024 ? new byte[128] : new byte[256]);
    //    //    byte[] pemPublicExponent = new byte[3];
    //    //    Array.Copy(keyData, (keySize1024 ? 29 : 33), pemModulus, 0, (keySize1024 ? 128 : 256));
    //    //    Array.Copy(keyData, (keySize1024 ? 159 : 291), pemPublicExponent, 0, 3);
    //    //    RSAParameters para = new RSAParameters();
    //    //    para.Modulus = pemModulus;
    //    //    para.Exponent = pemPublicExponent;
    //    //    return para;
    //    //}

    //    private static RSA GetRSAFromkey(string xmlkey)
    //    {
    //        var rsa = new RSACryptoServiceProvider();
    //        rsa.FromXmlString(xmlkey);
    //        return rsa;
    //    }
    //    #endregion

    //    #region 公开函数

    //    ///// <summary>
    //    ///// 加密
    //    ///// </summary>
    //    ///// <param name="contents">要加密的数据</param>
    //    ///// <param name="sPublicKey">公钥</param>
    //    ///// <returns></returns>
    //    //public static String Encrypt(string contents, string sPublicKey)
    //    //{
    //    //    using (RSACryptoServiceProvider RSACryptography = new RSACryptoServiceProvider())
    //    //    {
    //    //        Byte[] PlaintextData = Encoding.UTF8.GetBytes(contents);
    //    //        RSACryptography.FromXmlString(sPublicKey);
    //    //        int MaxBlockSize = RSACryptography.KeySize / 8 - 11;    //加密块最大长度限制

    //    //        if (PlaintextData.Length <= MaxBlockSize)
    //    //            return Convert.ToBase64String(RSACryptography.Encrypt(PlaintextData, false));

    //    //        using (MemoryStream PlaiStream = new MemoryStream(PlaintextData))
    //    //        using (MemoryStream CrypStream = new MemoryStream())
    //    //        {
    //    //            Byte[] Buffer = new Byte[MaxBlockSize];
    //    //            int BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);

    //    //            while (BlockSize > 0)
    //    //            {
    //    //                Byte[] ToEncrypt = new Byte[BlockSize];
    //    //                Array.Copy(Buffer, 0, ToEncrypt, 0, BlockSize);

    //    //                Byte[] Cryptograph = RSACryptography.Encrypt(ToEncrypt, false);
    //    //                CrypStream.Write(Cryptograph, 0, Cryptograph.Length);

    //    //                BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);
    //    //            }

    //    //            return Convert.ToBase64String(CrypStream.ToArray(), Base64FormattingOptions.None);
    //    //        }
    //    //    }
    //    //}

    //    ///// <summary>
    //    ///// 解密
    //    ///// </summary>
    //    ///// <param name="contents">要加密的数据</param>
    //    ///// <param name="sPrivateKey">私钥</param>
    //    ///// <returns></returns>
    //    //public static String Decrypt(string contents, string sPrivateKey)
    //    //{
    //    //    using (RSACryptoServiceProvider RSACryptography = new RSACryptoServiceProvider())
    //    //    {
    //    //        RSACryptography.FromXmlString(sPrivateKey);
    //    //        Byte[] CiphertextData = Convert.FromBase64String(contents);
    //    //        int MaxBlockSize = RSACryptography.KeySize / 8;    //解密块最大长度限制

    //    //        if (CiphertextData.Length <= MaxBlockSize)
    //    //            return Encoding.UTF8.GetString(RSACryptography.Decrypt(CiphertextData, false));

    //    //        using (MemoryStream CrypStream = new MemoryStream(CiphertextData))
    //    //        using (MemoryStream PlaiStream = new MemoryStream())
    //    //        {
    //    //            Byte[] Buffer = new Byte[MaxBlockSize];
    //    //            int BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);

    //    //            while (BlockSize > 0)
    //    //            {
    //    //                Byte[] ToDecrypt = new Byte[BlockSize];
    //    //                Array.Copy(Buffer, 0, ToDecrypt, 0, BlockSize);

    //    //                Byte[] Plaintext = RSACryptography.Decrypt(ToDecrypt, false);
    //    //                PlaiStream.Write(Plaintext, 0, Plaintext.Length);

    //    //                BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);
    //    //            }

    //    //            return Encoding.UTF8.GetString(PlaiStream.ToArray());
    //    //        }
    //    //    }
    //    //}

    //    /// <summary>
    //    /// 生成密钥对
    //    /// generate private key and public key arr[0] for private key arr[1] for public key
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string[] GenerateKeys()
    //    {
    //        string[] sKeys = new String[2];
    //        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
    //        //rSAParameters = rsa.ExportParameters(false);
    //        sKeys[0] = rsa.ToXmlString(true);
    //        sKeys[1] = rsa.ToXmlString(false);
    //        return sKeys;
    //    }

    //    /// <summary>
    //    /// 公钥加密
    //    /// </summary>
    //    /// <param name="rawInput"></param>
    //    /// <param name="publicKey"></param>
    //    /// <returns></returns>
    //    public static string EncryptPublickey(string data, string publicKey)
    //    {
    //        if (string.IsNullOrEmpty(data))
    //        {
    //            return string.Empty;
    //        }

    //        if (string.IsNullOrWhiteSpace(publicKey))
    //        {
    //            throw new ArgumentException("Invalid Public Key");
    //        }

    //        using (var rsaProvider = new RSACryptoServiceProvider())
    //        {
    //            var inputBytes = Encoding.UTF8.GetBytes(data);//有含义的字符串转化为字节流
    //            rsaProvider.FromXmlString(publicKey);//载入公钥
    //            int bufferSize = (rsaProvider.KeySize / 8) - 11;//单块最大长度
    //            var buffer = new byte[bufferSize];
    //            using (MemoryStream inputStream = new MemoryStream(inputBytes),
    //                 outputStream = new MemoryStream())
    //            {
    //                while (true)
    //                { //分段加密
    //                    int readSize = inputStream.Read(buffer, 0, bufferSize);
    //                    if (readSize <= 0)
    //                    {
    //                        break;
    //                    }

    //                    var temp = new byte[readSize];
    //                    Array.Copy(buffer, 0, temp, 0, readSize);
    //                    var encryptedBytes = rsaProvider.Encrypt(temp, false);
    //                    outputStream.Write(encryptedBytes, 0, encryptedBytes.Length);
    //                }
    //                return Convert.ToBase64String(outputStream.ToArray());//转化为字节流方便传输
    //            }
    //        }
    //    }
    //    /// <summary>
    //    /// 私钥解密
    //    /// </summary>
    //    /// <param name="encryptedInput"></param>
    //    /// <param name="privateKey"></param>
    //    /// <returns></returns>
    //    public static string Decryptprivekey(string encryptedInput, string privateKey)
    //    {
    //        if (string.IsNullOrEmpty(encryptedInput))
    //        {
    //            return string.Empty;
    //        }

    //        if (string.IsNullOrWhiteSpace(privateKey))
    //        {
    //            throw new ArgumentException("Invalid Private Key");
    //        }

    //        using (var rsaProvider = new RSACryptoServiceProvider())
    //        {
    //            var inputBytes = Convert.FromBase64String(encryptedInput);
    //            rsaProvider.FromXmlString(privateKey);
    //            int bufferSize = rsaProvider.KeySize / 8;
    //            var buffer = new byte[bufferSize];
    //            using (MemoryStream inputStream = new MemoryStream(inputBytes),
    //                 outputStream = new MemoryStream())
    //            {
    //                while (true)
    //                {
    //                    int readSize = inputStream.Read(buffer, 0, bufferSize);
    //                    if (readSize <= 0)
    //                    {
    //                        break;
    //                    }

    //                    var temp = new byte[readSize];
    //                    Array.Copy(buffer, 0, temp, 0, readSize);
    //                    var rawBytes = rsaProvider.Decrypt(temp, false);
    //                    outputStream.Write(rawBytes, 0, rawBytes.Length);
    //                }
    //                return Encoding.UTF8.GetString(outputStream.ToArray());
    //            }
    //        }
    //    }


    //    /// <summary>
    //    /// RSA加密 使用私钥加密
    //    /// </summary>
    //    /// <param name="byteData"></param>
    //    /// <param name="key"></param>
    //    /// <returns></returns>
    //    public static string EncryptPrivekey(string data, string key)
    //    {
    //        byte[] byteData = Encoding.UTF8.GetBytes(data);
    //        var privateRsa = GetRSAFromkey(key);
    //        //转换密钥  下面的DotNetUtilities来自Org.BouncyCastle.Security
    //        var keyPair = DotNetUtilities.GetKeyPair(privateRsa);

    //        var c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");

    //        c.Init(true, keyPair.Private);//取私钥（true为加密）


    //        int bufferSize = (privateRsa.KeySize / 8) - 11;//单块最大长度
    //        var buffer = new byte[bufferSize];
    //        using (MemoryStream inputStream = new MemoryStream(byteData), outputStream = new MemoryStream())
    //        {
    //            while (true)
    //            { //分段加密
    //                int readSize = inputStream.Read(buffer, 0, bufferSize);
    //                if (readSize <= 0)
    //                {
    //                    break;
    //                }

    //                var temp = new byte[readSize];
    //                Array.Copy(buffer, 0, temp, 0, readSize);
    //                //var encryptedBytes = rsaProvider.Encrypt(temp, false);
    //                var encryptedBytes = c.DoFinal(temp);
    //                outputStream.Write(encryptedBytes, 0, encryptedBytes.Length);
    //            }
    //            return Convert.ToBase64String(outputStream.ToArray());//转化为字节流方便传输
    //        }

    //    }

    //    /// <summary>
    //    /// RSA解密 使用公钥解密
    //    /// </summary>
    //    /// <param name="byteData"></param>
    //    /// <param name="key"></param>
    //    /// <returns></returns>
    //    public static string DecryptPublickey(string data, string key)
    //    {
    //        byte[] byteData = Convert.FromBase64String(data);
    //        var privateRsa = GetRSAFromkey(key);
    //        //转换密钥  
    //        var keyPair = DotNetUtilities.GetRsaPublicKey(privateRsa);

    //        var c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");

    //        c.Init(false, keyPair);//取公钥（false为解密）

    //        using (MemoryStream inputStream = new MemoryStream(byteData), outputStream = new MemoryStream())
    //        {
    //            int restLength = byteData.Length;
    //            while (restLength > 0)
    //            {
    //                int readLength = restLength < privateRsa.KeySize / 8 ? restLength : privateRsa.KeySize / 8;
    //                restLength = restLength - readLength;
    //                byte[] readBytes = new byte[readLength];
    //                inputStream.Read(readBytes, 0, readLength);
    //                byte[] append = c.DoFinal(readBytes);
    //                outputStream.Write(append, 0, append.Length);
    //            }
    //            //注意，这里不一定就是用utf8的编码方式,这个主要看加密的时候用的什么编码方式
    //            return Encoding.UTF8.GetString(outputStream.ToArray());
    //        }

    //    }

    //    public static string GetHash(string strSource)
    //    {
    //        try
    //        {
    //            //从字符串中取得Hash描述 
    //            byte[] Buffer;
    //            byte[] HashData;
    //            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
    //            Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(strSource);
    //            HashData = MD5.ComputeHash(Buffer);
    //            return Convert.ToBase64String(HashData);

    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    /// <summary>
    //    /// 签名
    //    /// </summary>
    //    /// <param name="p_strKeyPrivate">私钥</param>
    //    /// <param name="m_strHashbyteSignature">需签名的数据</param>
    //    /// <returns>签名后的值</returns>
    //    public static string Sign(string privekey, string data)
    //    {
    //        string hash = GetHash(data);
    //        byte[] rgbHash = Convert.FromBase64String(hash);
    //        //byte[] rgbHash = Encoding.UTF8.GetBytes(m_strHashbyteSignature);
    //        RSACryptoServiceProvider key = new RSACryptoServiceProvider();
    //        key.FromXmlString(privekey);
    //        RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
    //        formatter.SetHashAlgorithm("MD5");
    //        byte[] inArray = formatter.CreateSignature(rgbHash);
    //        return Convert.ToBase64String(inArray);
    //        //return Encoding.UTF8.GetString(inArray);
    //    }

    //    /// <summary>
    //    /// 签名验证
    //    /// </summary>
    //    /// <param name="publickey">公钥</param>
    //    /// <param name="data">待验证的数据</param>
    //    /// <param name="signstr">签名后的数据</param>
    //    /// <returns>签名是否符合</returns>
    //    public static bool DeSign(string publickey, string data, string signstr)
    //    {
    //        try
    //        {
    //            string hash = GetHash(data);
    //            byte[] rgbHash = Convert.FromBase64String(hash);
    //            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
    //            key.FromXmlString(publickey);
    //            RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
    //            deformatter.SetHashAlgorithm("MD5");
    //            byte[] rgbSignature = Convert.FromBase64String(signstr);
    //            if (deformatter.VerifySignature(rgbHash, rgbSignature))
    //            {
    //                return true;
    //            }
    //            return false;
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //    }
    //    /// <summary>
    //    /// 获取Hash描述表
    //    /// </summary>
    //    /// <param name="strSource">待签名的字符串</param>
    //    /// <param name="strHashData">Hash描述</param>
    //    /// <returns></returns>
    //    //public static bool GetHash(string strSource, ref string strHashData)
    //    //{
    //    //    try
    //    //    {
    //    //        //从字符串中取得Hash描述 
    //    //        byte[] Buffer;
    //    //        byte[] HashData;
    //    //        System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
    //    //        Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(strSource);
    //    //        HashData = MD5.ComputeHash(Buffer);
    //    //        strHashData = Convert.ToBase64String(HashData);
    //    //        return true;
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        throw ex;
    //    //    }
    //    //}
    //    ///// <summary>
    //    ///// RSA签名
    //    ///// </summary>
    //    ///// <param name="strKeyPrivate">私钥</param>
    //    ///// <param name="strHashbyteSignature">待签名Hash描述</param>
    //    ///// <param name="strEncryptedSignatureData">签名后的结果</param>
    //    ///// <returns></returns>
    //    //public static bool Sign(string strKeyPrivate, string strHashbyteSignature, ref string strEncryptedSignatureData)
    //    //{
    //    //    try
    //    //    {
    //    //        byte[] HashbyteSignature;
    //    //        byte[] EncryptedSignatureData;
    //    //        HashbyteSignature = Convert.FromBase64String(strHashbyteSignature);
    //    //        System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
    //    //        RSA.FromXmlString(strKeyPrivate);
    //    //        System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
    //    //        //设置签名的算法为MD5 
    //    //        RSAFormatter.SetHashAlgorithm("MD5");
    //    //        //执行签名 
    //    //        EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
    //    //        strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);
    //    //        return true;
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        throw ex;
    //    //    }
    //    //}
    //    ///// <summary>
    //    ///// RSA签名验证
    //    ///// </summary>
    //    ///// <param name="strKeyPublic">公钥</param>
    //    ///// <param name="strHashbyteDeformatter">Hash描述</param>
    //    ///// <param name="strDeformatterData">签名后的结果</param>
    //    ///// <returns></returns>
    //    //public static bool DeSign(string strKeyPublic, string strHashbyteDeformatter, string strDeformatterData)
    //    //{
    //    //    try
    //    //    {
    //    //        byte[] DeformatterData;
    //    //        byte[] HashbyteDeformatter;
    //    //        HashbyteDeformatter = Convert.FromBase64String(strHashbyteDeformatter);
    //    //        System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
    //    //        RSA.FromXmlString(strKeyPublic);
    //    //        System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
    //    //        //指定解密的时候HASH算法为MD5 
    //    //        RSADeformatter.SetHashAlgorithm("MD5");
    //    //        DeformatterData = Convert.FromBase64String(strDeformatterData);
    //    //        if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
    //    //        {
    //    //            return true;
    //    //        }
    //    //        else
    //    //        {
    //    //            return false;
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        throw ex;
    //    //    }
    //    //}
    //    #endregion
    //}
}
