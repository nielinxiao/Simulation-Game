using System;
using YIUIBind;
using YIUIFramework;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace YIUI.GameStart
{
    /// <summary>
    /// Author  YIUI
    /// Date    2026.2.12
    /// </summary>
    public sealed partial class GameStartPanel:GameStartPanelBase
    {
    
        #region 生命周期
        
        protected override void Initialize()
        {
            Debug.Log($"GameStartPanel Initialize");
        }

        protected override void Start()
        {
            Debug.Log($"GameStartPanel Start");
        }

        protected override void OnEnable()
        {
            Debug.Log($"GameStartPanel OnEnable");
        }

        protected override void OnDisable()
        {
            Debug.Log($"GameStartPanel OnDisable");
        }

        protected override void OnDestroy()
        {
            Debug.Log($"GameStartPanel OnDestroy");
        }

        protected override async UniTask<bool> OnOpen()
        {
            await UniTask.CompletedTask;
            Debug.Log($"GameStartPanel OnOpen");
            return true;
        }

        protected override async UniTask<bool> OnOpen(ParamVo param)
        {
            return await base.OnOpen(param);
        }
        
        #endregion

        #region Event开始


        #endregion Event结束

    }
}