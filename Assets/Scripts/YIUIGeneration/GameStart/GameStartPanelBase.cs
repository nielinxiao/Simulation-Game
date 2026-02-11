using System;
using YIUIBind;
using YIUIFramework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace YIUI.GameStart
{



    /// <summary>
    /// 由YIUI工具自动创建 请勿手动修改
    /// </summary>
    public abstract class GameStartPanelBase:BasePanel
    {
        public const string PkgName = "GameStart";
        public const string ResName = "GameStartPanel";
        
        public override EWindowOption WindowOption => EWindowOption.None;
        public override EPanelLayer Layer => EPanelLayer.Panel;
        public override EPanelOption PanelOption => EPanelOption.None;
        public override EPanelStackOption StackOption => EPanelStackOption.VisibleTween;
        public override int Priority => 0;

        
        protected sealed override void UIBind()
        {

        }

        protected sealed override void UnUIBind()
        {

        }
     
   
   
    }
}