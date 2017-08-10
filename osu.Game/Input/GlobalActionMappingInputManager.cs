// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Input;

namespace osu.Game.Input
{
    public class GlobalActionMappingInputManager : ActionMappingInputManager<GlobalAction>
    {
        private readonly Drawable handler;

        public GlobalActionMappingInputManager(OsuGameBase game)
        {
            if (game is IHandleActions<GlobalAction>)
                handler = game;
        }

        protected override IDictionary<KeyCombination, GlobalAction> CreateDefaultMappings() => new Dictionary<KeyCombination, GlobalAction>
        {
            { Key.F8, GlobalAction.ToggleChat },
            { Key.F9, GlobalAction.ToggleSocial },
            { new[] { Key.LControl, Key.LAlt, Key.R }, GlobalAction.ResetInputSettings },
            { new[] { Key.LControl, Key.T }, GlobalAction.ToggleToolbar },
            { new[] { Key.LControl, Key.O }, GlobalAction.ToggleSettings },
            { new[] { Key.LControl, Key.D }, GlobalAction.ToggleDirect },
        };

        protected override bool PropagateKeyDown(IEnumerable<Drawable> drawables, InputState state, KeyDownEventArgs args)
        {
            if (handler != null)
                drawables = new[] { handler }.Concat(drawables);

            // always handle ourselves before all children.
            return base.PropagateKeyDown(drawables, state, args);
        }

        protected override bool PropagateKeyUp(IEnumerable<Drawable> drawables, InputState state, KeyUpEventArgs args)
        {
            if (handler != null)
                drawables = new[] { handler }.Concat(drawables);

            // always handle ourselves before all children.
            return base.PropagateKeyUp(drawables, state, args);
        }
    }

    public enum GlobalAction
    {
        [Description("Toggle chat overlay")]
        ToggleChat,
        [Description("Toggle social overlay")]
        ToggleSocial,
        [Description("Reset input settings")]
        ResetInputSettings,
        [Description("Toggle toolbar")]
        ToggleToolbar,
        [Description("Toggle settings")]
        ToggleSettings,
        [Description("Toggle osu!direct")]
        ToggleDirect,
    }
}
