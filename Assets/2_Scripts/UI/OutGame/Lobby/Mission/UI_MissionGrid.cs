﻿using System;
using _2_Scripts.Game.BackEndData.Mission;
using UnityEngine;
using UnityEngine.UI.Extensions;
using UnityEngine.UI.Extensions.EasingCore;

namespace _2_Scripts.UI.OutGame.Lobby
{
    public enum Alignment
    {
        Upper,
        Middle,
        Lower,
    }

    public class Context : FancyGridViewContext
    {
        public int SelectedIndex = -1;
        public Action<int> OnCellClicked;
    }
    
    public class UI_MissionGrid: FancyGridView<SpawnMission, Context>
    {
        class CellGroup : DefaultCellGroup { }
        
        [SerializeField] private UI_MissionCharacterCard cellPrefab;
        protected override void SetupCellTemplate() => Setup<CellGroup>(cellPrefab);

        public float PaddingTop
        {
            get => paddingHead;
            set
            {
                paddingHead = value;
                Relayout();
            }
        }

        public float PaddingBottom
        {
            get => paddingTail;
            set
            {
                paddingTail = value;
                Relayout();
            }
        }

        public float SpacingY
        {
            get => spacing;
            set
            {
                spacing = value;
                Relayout();
            }
        }

        public float SpacingX
        {
            get => startAxisSpacing;
            set
            {
                startAxisSpacing = value;
                Relayout();
            }
        }

        public void UpdateSelection(int index)
        {
            if (Context.SelectedIndex == index)
            {
                return;
            }

            Context.SelectedIndex = index;
            Refresh();
        }

        public void OnCellClicked(Action<int> callback)
        {
            Context.OnCellClicked = callback;
        }

        public void ScrollTo(int index, float duration, Ease easing, Alignment alignment = Alignment.Middle)
        {
            UpdateSelection(index);
            ScrollTo(index, duration, easing, GetAlignment(alignment));
        }

        public void JumpTo(int index, Alignment alignment = Alignment.Middle)
        {
            UpdateSelection(index);
            JumpTo(index, GetAlignment(alignment));
        }

        float GetAlignment(Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.Upper: return 0.0f;
                case Alignment.Middle: return 0.5f;
                case Alignment.Lower: return 1.0f;
                default: return GetAlignment(Alignment.Upper);
            }
        }
    }
}
