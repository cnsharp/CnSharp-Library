using System;
using System.Security.Cryptography;
using System.Text;

namespace CnSharp.Security
{
	/// <summary>
	/// 3DES �����㷨
	/// </summary>
	public static class TripleDES
	{

		#region Public Methods

		/// <summary>
		/// 3des�����ַ���
		/// </summary>
		/// <param name="entryptText">����</param>
		/// <param name="encoding">���뷽ʽ</param>
		/// <returns>���ܺ���ַ���</returns>
		/// <remarks>��̬������ָ�����뷽ʽ</remarks>
        public static string Decrypt3DES(string entryptText, string key, Encoding encoding = null)
		{
		    if (encoding == null)
		        encoding = Encoding.UTF8;
			var des = new TripleDESCryptoServiceProvider();
			var hashMD5 = new MD5CryptoServiceProvider();

			des.Key = hashMD5.ComputeHash(encoding.GetBytes(key));
			des.Mode = CipherMode.ECB;

			ICryptoTransform desDecrypt = des.CreateDecryptor();

			string result = "";
			try
			{
				byte[] buffer = Convert.FromBase64String(entryptText);
				result = encoding.GetString(desDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
			}
			catch (Exception e)
			{
				throw (new Exception("Invalid Key or input string is not a valid base64 string", e));
			}

			return result;
		}

		/// <summary>
		/// 3des�����ַ���
		/// </summary>
		/// <param name="plainText">����</param>
		/// <param name="encoding">���뷽ʽ</param>
		/// <returns>���ܺ󲢾�base63������ַ���</returns>
		/// <remarks>���أ�ָ�����뷽ʽ</remarks>
		public static string Encrypt3DES(string plainText,string key, Encoding encoding = null)
		{
            if (encoding == null)
                encoding = Encoding.UTF8;
			var des = new TripleDESCryptoServiceProvider();
			var hashMD5 = new MD5CryptoServiceProvider();

			des.Key = hashMD5.ComputeHash(encoding.GetBytes(key));
			des.Mode = CipherMode.ECB;

			ICryptoTransform desEncrypt = des.CreateEncryptor();

			byte[] Buffer = encoding.GetBytes(plainText);
			return Convert.ToBase64String(desEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
		}

		#endregion
	}
}