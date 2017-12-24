namespace Weixin.Next.MP.Messaging.Responses
{
    public interface IResponseMessage
    {
        /// <summary>
        /// ���ظ�΢��ʱ�Ƿ���Ҫ�ȼ���
        /// </summary>
        bool EncryptionRequired { get; }
        /// <summary>
        /// ת��Ϊ����΢�ŷ���������Ϣ�ı�
        /// </summary>
        /// <returns></returns>
        string Serialize();
    }

    /// <summary>
    /// ������Ҫֱ�ӷ����ַ���, �����Ǹ�ʽ���� xml ��Ϣ
    /// </summary>
    public class RawResponseMessage : IResponseMessage
    {
        private readonly string _message;

        public RawResponseMessage(string message)
        {
            _message = message;
        }

        /// <summary>
        /// ����ֱ�ӷ��ص��ַ���
        /// </summary>
        public string Message
        {
            get { return _message; }
        }

        public bool EncryptionRequired
        {
            get { return false; }
        }

        public string Serialize()
        {
            return Message;
        }

        public static readonly RawResponseMessage Empty = new RawResponseMessage("");
        public static readonly RawResponseMessage Success = new RawResponseMessage("success");
    }
}