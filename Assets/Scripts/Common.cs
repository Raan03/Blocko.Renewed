namespace Assets.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    public static class Common
    {
        #region Public Methods

        public static int GenerateDynamicFontSize(int fontSize)
        {
            return Mathf.Min(Screen.height, Screen.width) / fontSize;
        }

        #endregion Public Methods
    }
}