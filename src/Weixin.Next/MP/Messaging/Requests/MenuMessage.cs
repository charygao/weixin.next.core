using System;
using System.Linq;
using System.Xml.Linq;

namespace Weixin.Next.MP.Messaging.Requests
{
    public abstract class MenuMessage : KeyedEventMessage
    {
        public MenuMessage(XElement xml) : base(xml)
        {
        }

        public long MenuId { get { return long.Parse(_xml.Element("MenuId").Value); } }

        public static MenuMessage Parse(XElement xml, EventMessageType @event)
        {
            switch (@event)
            {

                case EventMessageType.click:
                    return new ClickMenuMessage(xml);
                case EventMessageType.view:
                    return new ViewMenuMessage(xml);
                case EventMessageType.scancode_push:
                    return new ScanCodePushMenuMessage(xml);
                case EventMessageType.scancode_waitmsg:
                    return new ScanCodeWaitMsgMenuMessage(xml);
                case EventMessageType.pic_sysphoto:
                    return new PicSysPhotoMenuMessage(xml);
                case EventMessageType.pic_photo_or_album:
                    return new PicPhotoOrAlbumMenuMessage(xml);
                case EventMessageType.pic_weixin:
                    return new PicWeixinMenuMessage(xml);
                case EventMessageType.location_select:
                    return new LocationSelectMenuMessage(xml);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// click ����˵���ȡ��Ϣ
    /// <para>
    /// EventKey: ���Զ���˵��ӿ���KEYֵ��Ӧ
    /// </para>
    /// </summary>
    public class ClickMenuMessage : MenuMessage
    {
        public ClickMenuMessage(XElement xml) : base(xml)
        {
        }
    }

    /// <summary>
    /// view ����˵���ȡ��Ϣ
    /// <para>
    /// EventKey: ���õ���תURL
    /// </para>
    /// </summary>
    public class ViewMenuMessage : MenuMessage
    {
        public ViewMenuMessage(XElement xml) : base(xml)
        {
        }
    }

    #region scan

    public class ScanCodeInfo
    {
        private readonly XElement _element;

        public ScanCodeInfo(XElement element)
        {
            _element = element;
        }

        /// <summary>
        /// ɨ�����ͣ�һ����qrcode
        /// </summary>
        public string ScanType { get { return _element.Element("ScanType").Value; } }

        /// <summary>
        /// ɨ����������ά���Ӧ���ַ�����Ϣ
        /// </summary>
        public string ScanResult { get { return _element.Element("ScanResult").Value; } }
    }

    public abstract class ScanCodeMenuMessage : MenuMessage
    {
        public ScanCodeMenuMessage(XElement xml) : base(xml)
        {
        }

        /// <summary>
        /// ɨ����Ϣ
        /// </summary>
        public ScanCodeInfo ScanCodeInfo { get { return new ScanCodeInfo(_xml.Element("ScanCodeInfo")); } }
    }

    /// <summary>
    /// scancode_push ɨ�����¼����¼�����
    /// <para>
    /// EventKey: �¼�KEYֵ���ɿ������ڴ����˵�ʱ�趨
    /// </para>
    /// </summary>
    public class ScanCodePushMenuMessage : ScanCodeMenuMessage
    {
        public ScanCodePushMenuMessage(XElement xml) : base(xml)
        {
        }
    }

    /// <summary>
    /// scancode_waitmsg ɨ�����¼��ҵ�������Ϣ�����С���ʾ����¼�����
    /// <para>
    /// EventKey: �¼�KEYֵ���ɿ������ڴ����˵�ʱ�趨
    /// </para>
    /// </summary>
    public class ScanCodeWaitMsgMenuMessage : ScanCodeMenuMessage
    {
        public ScanCodeWaitMsgMenuMessage(XElement xml) : base(xml)
        {
        }
    }

    #endregion

    #region pic

    public class PicItem
    {
        private readonly XElement _element;

        public PicItem(XElement element)
        {
            _element = element;
        }

        /// <summary>
        /// ͼƬ��MD5ֵ������������Ҫ����������֤���յ�ͼƬ
        /// </summary>
        public string PicMd5Sum { get { return _element.Value; } }
    }

    public class SendPicsInfo
    {
        private readonly XElement _element;

        public SendPicsInfo(XElement element)
        {
            _element = element;
        }

        /// <summary>
        /// ���͵�ͼƬ����
        /// </summary>
        public int Count { get { return int.Parse(_element.Element("Count").Value); } }

        /// <summary>
        /// ͼƬ�б�
        /// </summary>
        public PicItem[] PicList
        {
            get
            {
                return _element.Element("PicList")
                    .Elements("item")
                    .Select(x => new PicItem(x))
                    .ToArray();
            }
        }
    }

    public abstract class PicMenuMessage : MenuMessage
    {
        public PicMenuMessage(XElement xml) : base(xml)
        {
        }

        /// <summary>
        /// ���͵�ͼƬ��Ϣ
        /// </summary>
        public SendPicsInfo SendPicsInfo { get { return new SendPicsInfo(_xml.Element("SendPicsInfo")); } }
    }

    /// <summary>
    /// pic_sysphoto ����ϵͳ���շ�ͼ���¼�����
    /// <para>
    /// EventKey: �¼�KEYֵ���ɿ������ڴ����˵�ʱ�趨
    /// </para>
    /// </summary>
    public class PicSysPhotoMenuMessage : PicMenuMessage
    {
        public PicSysPhotoMenuMessage(XElement xml) : base(xml)
        {
        }
    }

    /// <summary>
    /// pic_photo_or_album �������ջ�����ᷢͼ���¼�����
    /// <para>
    /// EventKey: �¼�KEYֵ���ɿ������ڴ����˵�ʱ�趨
    /// </para>
    /// </summary>
    public class PicPhotoOrAlbumMenuMessage : PicMenuMessage
    {
        public PicPhotoOrAlbumMenuMessage(XElement xml) : base(xml)
        {
        }
    }

    /// <summary>
    /// pic_weixin ����΢����ᷢͼ�����¼�����
    /// <para>
    /// EventKey: �¼�KEYֵ���ɿ������ڴ����˵�ʱ�趨
    /// </para>
    /// </summary>
    public class PicWeixinMenuMessage : PicMenuMessage
    {
        public PicWeixinMenuMessage(XElement xml) : base(xml)
        {
        }
    }

    #endregion

    public class SendLocationInfo
    {
        private readonly XElement _element;

        public SendLocationInfo(XElement element)
        {
            _element = element;
        }

        /// <summary>
        /// X������Ϣ
        /// </summary>
        public double Location_X { get { return double.Parse(_element.Element("Location_X").Value); } }

        /// <summary>
        /// Y������Ϣ
        /// </summary>
        public double Location_Y { get { return double.Parse(_element.Element("Location_Y").Value); } }

        /// <summary>
        /// ���ȣ������Ϊ���Ȼ��߱����ߡ�Խ��ϸ�Ļ� scaleԽ��
        /// </summary>
        public int Scale { get { return int.Parse(_element.Element("Scale").Value); } }

        /// <summary>
        /// ����λ�õ��ַ�����Ϣ
        /// </summary>
        public string Label { get { return _element.Element("Label").Value; } }

        /// <summary>
        /// ����ȦPOI�����֣�����Ϊ��
        /// </summary>
        public string Poiname { get { return _element.Element("Poiname").Value; } }
    }

    /// <summary>
    /// location_select ��������λ��ѡ�������¼�����
    /// <para>
    /// EventKey: �¼�KEYֵ���ɿ������ڴ����˵�ʱ�趨
    /// </para>
    /// </summary>
    public class LocationSelectMenuMessage : MenuMessage
    {
        public LocationSelectMenuMessage(XElement xml) : base(xml)
        {
        }

        /// <summary>
        /// ���͵�λ����Ϣ
        /// </summary>
        public SendLocationInfo SendLocationInfo { get { return new SendLocationInfo(_xml.Element("SendLocationInfo")); } }
    }
}