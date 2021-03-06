﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;
using BGC.Utility;

namespace BGC.Web.Utility
{
    public static class Rest
    {
        /// <summary>
        /// Send a post request
        /// </summary>
        /// <param name="callBack">true means there was an error</param>
        public static void PostRequest(
            string url,
            Dictionary<string, string> headers, 
            string body, 
            Action<UnityWebRequest, bool> callBack = null)
        {
            CoroutineUtility.Mono.StartCoroutine(RunPost(
                url,
                headers,
                body,
                callBack));
        }

        /// <summary>
        /// Run post request
        /// </summary>
        /// <param name="callBack">boolean true means there was an error</param>
        /// <returns></returns>
        private static IEnumerator RunPost(
            string url, 
            Dictionary<string, string> headers, 
            string body,
            Action<UnityWebRequest, bool> callBack)
        {
            UnityWebRequest request = UnityWebRequest.Post(url, "");
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));

            foreach (KeyValuePair<string, string> pair in headers)
            {
                request.SetRequestHeader(pair.Key, pair.Value);
            }

            yield return request.SendWebRequest();
            callBack?.Invoke(request, true);
        }
    }
}