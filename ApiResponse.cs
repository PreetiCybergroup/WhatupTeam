using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WhatupTeam
{
    public static class ApiResponse
    {
        public static bool getResponse(string serviceName)
        { 
           HttpClient httpClient = new HttpClient();
           httpClient.BaseAddress = new Uri(Convert.ToString(ConfigurationManager.AppSettings["ApiUrl"]));
           httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
           HttpResponseMessage response = httpClient.GetAsync(string.Format("api/{0}", serviceName)).Result;
           return response.IsSuccessStatusCode;
        }
    }
}