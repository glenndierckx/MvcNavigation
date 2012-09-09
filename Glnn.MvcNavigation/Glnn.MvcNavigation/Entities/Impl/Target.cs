using System;

namespace Glnn.MvcNavigation.Entities.Impl
{
    public class Target : IEquatable<Target>
    {
        private readonly string _controllerLowered;
        private readonly string _actionLowered;
        private readonly string _controller;
        private readonly string _action;
        private readonly bool _activeOnControllerOnly;

        public Target(string controller) : this (controller, "Index")
        {
            _activeOnControllerOnly = true;
        }
        public Target(string controller, string action)
        {
            _controller = controller;
            _controllerLowered = controller.ToLower();
            _action = action;
            _actionLowered = action.ToLower();
        }

        public string Controller
        {
            get { return _controller; }
        }
        public string Action
        {
            get { return _action; }
        }

        public bool Equals(Target other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(_controllerLowered, other._controllerLowered)
                && (_activeOnControllerOnly || string.Equals(_actionLowered, other._actionLowered));
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Target) obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Controller != null ? Controller.ToLower().GetHashCode() : 0)*397) ^ (Action != null ? Action.ToLower().GetHashCode() : 0);
            }
        }
        public static bool operator ==(Target left, Target right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(Target left, Target right)
        {
            return !Equals(left, right);
        }
    }
}