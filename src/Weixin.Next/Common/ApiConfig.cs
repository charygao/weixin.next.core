﻿using System.Net.Http;

namespace Weixin.Next.Common
{
    public class ApiConfig
    {
        /// <summary>
        /// 用于将请求对象转换为 JSON 字符串；以及将返回结果转换为对象
        /// </summary>
        public IJsonParser JsonParser { get; set; }
        /// <summary>
        /// 用于发送网络请求, 日志代码可以通过对 HttpClient 的自定义来记录日志
        /// </summary>
        public HttpClient HttpClient { get; set; }
        /// <summary>
        /// 用于管理 access_token
        /// </summary>
        public IAccessTokenManager AccessTokenManager { get; set; }
    }
}
