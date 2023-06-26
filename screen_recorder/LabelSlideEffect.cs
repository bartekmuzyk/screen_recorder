using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace screen_recorder
{
    internal class LabelSlideEffectState
    {
        public const int STARTING_SLIDE_COUNTER_VALUE = -5;

        public readonly Label Target;

        public int Counter = STARTING_SLIDE_COUNTER_VALUE;

        public string SourceText = string.Empty;

        public LabelSlideEffectState(Label target)
        {
            Target = target;
        }
    }

    internal class LabelSlideEffect
    {
        private readonly Dictionary<string, LabelSlideEffectState> states = new();

        public void AddLabel(Label label)
        {
            states[label.Name] = new LabelSlideEffectState(label);
        }

        public void SetText(Label label, string text)
        {
            if (!states.TryGetValue(label.Name, out _)) return;

            var state = states[label.Name];
            state.SourceText = text;
            state.Counter = LabelSlideEffectState.STARTING_SLIDE_COUNTER_VALUE;
            state.Target.Text = text;
        }

        public void Tick()
        {
            foreach (var state in states.Values)
            {
                if (state.SourceText == string.Empty)
                {
                    if (state.Target.Text != string.Empty)
                    {
                        state.Target.Text = string.Empty;
                    }

                    continue;
                }

                state.Target.Text = state.Counter >= 0 ?
                    state.SourceText[state.Counter..]
                    :
                    state.SourceText;

                if (state.Counter == state.SourceText.Length - 1)
                {
                    state.Counter = LabelSlideEffectState.STARTING_SLIDE_COUNTER_VALUE;
                }
                else
                {
                    state.Counter++;
                }
            }
        }
    }
}
