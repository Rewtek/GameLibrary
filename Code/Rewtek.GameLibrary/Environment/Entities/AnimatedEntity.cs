namespace Rewtek.GameLibrary.Environment.Entities
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Rewtek.GameLibrary.Rendering.Sprites;

    public abstract class AnimatedEntity
    {
        // Variables
        private readonly List<Animation> _animations;

        // Properties
        public int AnimationCount { get { return _animations.Count; } }
        public string CurrentAnimation { get; private set; }

        // Constructor
        public AnimatedEntity()
        {
            _animations = new List<Animation>();
        }

        // Methods
        protected void AddAnimation(Animation animation)
        {
            if (!_animations.Contains(animation))
            {
                _animations.Add(animation);
            }
            else
            {
                Logger.Debug("Entity already contains this animation ({0})", animation.Name);
            }
        }

        public bool HasAnimation(string animation)
        {
            return _animations.Find(match => match.Name == animation) != null;
        }

        protected void RemoveAnimation(Animation animation)
        {
            _animations.Remove(animation);
        }

        protected void RemoveAnimation(string animation)
        {
            _animations.RemoveAll(match => match.Name == animation);
        }

        public Animation[] GetAnimations()
        {
            return _animations.ToArray();
        }        

        public void SetAnimation(string animation)
        {
            CurrentAnimation = animation;
        }

        public void StopAnimation()
        {
            CurrentAnimation = Animation.DefaultName;
        }
    }
}
