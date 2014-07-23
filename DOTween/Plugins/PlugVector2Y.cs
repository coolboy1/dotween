﻿// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2014/07/11 11:29
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 

using System;
using DG.Tweening.Core;
using DG.Tweening.Core.Easing;
using DG.Tweening.Plugins.Core;
using UnityEngine;

#pragma warning disable 1591
namespace DG.Tweening.Plugins
{
    // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // ||| PLUG ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public struct PlugVector2Y : IPlugSetter<Vector2, float, Vector2YPlugin, PlugVector2Y.Options>
    {
        readonly float _endValue;
        readonly DOGetter<Vector2> _getter;
        readonly DOSetter<Vector2> _setter;
        readonly Options _options;

        public PlugVector2Y(DOGetter<Vector2> getter, DOSetter<Vector2> setter, float endValue, Options options = new Options())
        {
            _getter = getter;
            _setter = setter;
            _endValue = endValue;
            _options = options;
        }

        public DOGetter<Vector2> Getter() { return _getter; }
        public DOSetter<Vector2> Setter() { return _setter; }
        public float EndValue() { return _endValue; }
        public Options GetOptions() { return _options; }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // ||| OPTIONS |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public struct Options
        {
            public bool snapping;

            public Options(bool snapping)
            {
                this.snapping = snapping;
            }
        }
    }

    // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // ||| PLUGIN ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    public class Vector2YPlugin : ABSTweenPlugin<Vector2, float, PlugVector2Y.Options>
    {
        public override float ConvertT1toT2(PlugVector2Y.Options options, Vector2 value)
        {
            return value.y;
        }

        public override float GetRelativeEndValue(PlugVector2Y.Options options, float startValue, float changeValue)
        {
            return startValue + changeValue;
        }

        public override float GetChangeValue(PlugVector2Y.Options options, float startValue, float endValue)
        {
            return endValue - startValue;
        }

        public override Vector2 Evaluate(PlugVector2Y.Options options, Tween t, bool isRelative, DOGetter<Vector2> getter, float elapsed, float startValue, float changeValue, float duration)
        {
            Vector2 res = getter();
            res.y = Ease.Apply(t, elapsed, startValue, changeValue, duration, 0, 0);
            if (options.snapping) res.y = (float)Math.Round(res.y);
            return res;
        }
    }
}