using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore
{
    public static class CardActionType
    {
        public const string OPEN_URL = "openUrl";
        public const string IM_BACK = "imBack";
    }

    public enum AttachmentLayout
    {
        list,
        carousel
    }
}
