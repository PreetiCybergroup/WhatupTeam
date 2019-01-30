using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatupTeam.Models.Entities
{
   public class ActionResponseEnum
    {
        public enum ActionResponse
        {
            BadRequest,
            AddNew,
            Update,
            Delete,
            Error,
            AddExisting,
            NotFound
        }
        public readonly static Dictionary<ActionResponse, string> Message = new Dictionary<ActionResponse, string>
        {
            {ActionResponse.BadRequest, "Invalid client request" },
            {ActionResponse.AddNew, "New record has been added" },
            {ActionResponse.Update, "Existing record has been modified" },
            {ActionResponse.Delete, "Existing Company is removed" },
            {ActionResponse.Error, "Error in response" },
            {ActionResponse.NotFound, "Requested record doesn't exists" }
        };
    }

 
}