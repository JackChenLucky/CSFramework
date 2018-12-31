using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Service.Common.Helper
{
	/// <summary>
	/// ���ܡ�
	/// </summary>
	public class EncryptionHelper
	{
		public static string MD5Encryption(string plainText)
		{	
			return BitConverter.ToString(
				(new MD5CryptoServiceProvider()).ComputeHash(
				Encoding.ASCII.GetBytes(plainText)));
		}

		public static string RSAEncryption(string plainText)
		{			
			return BitConverter.ToString( 
				( new RSACryptoServiceProvider() ).Encrypt(
					Encoding.ASCII.GetBytes( plainText),false ));
		}

		public static string RSADecryption(string encryptedText)
		{
			return BitConverter.ToString( 
				( new RSACryptoServiceProvider() ).Decrypt(
				Encoding.ASCII.GetBytes( encryptedText),false ));
		}

		public static string DESEncryption(string plainText)
		{			
			return _enCrypt(new DESCryptoServiceProvider(),plainText);
		}

		public static string DESDecryption(string encryptedText)
		{
            return Encrypt.DecryptString(encryptedText);
			//return _deCrypt(new DESCryptoServiceProvider(),encryptedText);
            //�µĽ��ܷ�ʽ ϵͳֻ�õ�����
            //addby luozh 20130627


		}

		public static String EncryptionKey = typeof(System.IO.BinaryReader).ToString() + "-w9" +
			typeof(System.Xml.NameTable).ToString() + "sdf3f" + typeof(Random).ToString() + "jsow23j235ay2s" +
			typeof(EncryptionHelper).ToString() + "a2skwp230a" + typeof(System.Collections.Queue).ToString() + "�sdadjm" +
			typeof(System.NullReferenceException).ToString();

		#region implementation
		private static String _enCrypt(SymmetricAlgorithm Algorithm, String ValueToEnCrypt)
		{
			// ���ַ������浽�ֽ�������
			Byte [] InputByteArray = Encoding.UTF8.GetBytes(ValueToEnCrypt);

			// �����Ҫ����Կ
			String EncryptionKey = EncryptionHelper.EncryptionKey;
			
			// ����һ��key.
			Byte [] Key = ASCIIEncoding.ASCII.GetBytes(EncryptionKey);
			Algorithm.Key = (Byte [])ArrayFunctions.ReDim(Key, Algorithm.Key.Length);
			Algorithm.IV = (Byte [])ArrayFunctions.ReDim(Key, Algorithm.IV.Length);

			MemoryStream MemStream = new MemoryStream();
			CryptoStream CrypStream = new CryptoStream(MemStream, Algorithm.CreateEncryptor(), CryptoStreamMode.Write);
			
			// Write the byte array into the crypto stream( It will end up in the memory stream).
			CrypStream.Write(InputByteArray, 0, InputByteArray.Length);
			CrypStream.FlushFinalBlock();

			// Get the data back from the memory stream, and into a string.
			StringBuilder StringBuilder = new StringBuilder();
			for (Int32 i = 0; i < MemStream.ToArray().Length; i++)
			{
				Byte ActualByte = MemStream.ToArray()[i];
				
				// Format the actual byte as HEX.
				StringBuilder.AppendFormat("{0:X2}", ActualByte);
			}
			
			return StringBuilder.ToString();
		}


		private static String _deCrypt(SymmetricAlgorithm Algorithm, String ValueToDeCrypt)
		{
			// Put the input string into the byte array.
			Byte [] InputByteArray = new Byte[ValueToDeCrypt.Length / 2];

			for (Int32 i = 0; i < ValueToDeCrypt.Length / 2; i++)
			{
				Int32 Value = (Convert.ToInt32(ValueToDeCrypt.Substring(i * 2, 2), 16));
				InputByteArray[i] = (Byte)Value;
			}

			// Create the crypto objects.
			String EncryptionKey = EncryptionHelper.EncryptionKey;
			// Create the key.
			Byte [] Key = ASCIIEncoding.ASCII.GetBytes(EncryptionKey);
			Algorithm.Key = (Byte [])ArrayFunctions.ReDim(Key, Algorithm.Key.Length);
			Algorithm.IV = (Byte [])ArrayFunctions.ReDim(Key, Algorithm.IV.Length);
			
			MemoryStream MemStream = new MemoryStream();
			CryptoStream CrypStream = new CryptoStream(MemStream, Algorithm.CreateDecryptor(), CryptoStreamMode.Write);
			
			// Flush the data through the crypto stream into the memory stream.
			CrypStream.Write(InputByteArray, 0, InputByteArray.Length);
			CrypStream.FlushFinalBlock();

			// Get the decrypted data back from the memory stream.
			StringBuilder StringBuilder = new StringBuilder();
			
			for (Int32 i = 0; i < MemStream.ToArray().Length; i++)
			{
				StringBuilder.Append((Char)MemStream.ToArray()[i]);
			}

			return StringBuilder.ToString();
		}
		#endregion
	}

	public class ArrayFunctions : Object
	{
		/// <summary>
		/// ���¶���һ�������б�
		/// </summary>
		/// <param name="OriginalArray">��Ҫ���ض��������</param>
		/// <param name="NewSize">���������´�С</param>
		public static Array ReDim(Array OriginalArray, Int32 NewSize) 
		{
			Type ArrayElementsType = OriginalArray.GetType().GetElementType();
			Array newArray = Array.CreateInstance(ArrayElementsType, NewSize);
			Array.Copy(OriginalArray, 0, newArray, 0, Math.Min(OriginalArray.Length, NewSize));
			return newArray;
		}
	}

    public class CommonHelper
    {
        public static string Convert(string value)
        {
            return string.Format(value.Replace("[", "{0}").Replace("]", "{1}"), "[[]", "[]]").Replace("*", "[*]").Replace("%", "[%]").Replace("'", "''");
        }

        public static string ConvertXML(string value)
        {
            return value.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;");

            //return "![CDATA[" + value + "]]";
        }
    }
}
