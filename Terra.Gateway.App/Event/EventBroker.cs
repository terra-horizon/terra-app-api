
namespace Terra.Gateway.App.Event
{
	public class EventBroker
	{
		#region User Deleted

		private EventHandler<OnUserEventArgs> _userDeleted;
		public event EventHandler<OnUserEventArgs> UserDeleted
		{
			add { this._userDeleted += value; }
			remove { this._userDeleted -= value; }
		}

		public void EmitUserDeleted(OnUserEventArgs.UserIdentifier id)
		{
			this.EmitUserDeleted(this, new List<OnUserEventArgs.UserIdentifier>() { id });
		}

		public void EmitUserDeleted(IEnumerable<OnUserEventArgs.UserIdentifier> ids)
		{
			this.EmitUserDeleted(this, ids);
		}

		public void EmitUserDeleted(IEnumerable<OnUserEventArgs> events)
		{
			this.EmitUserDeleted(this, events);
		}

		public void EmitUserDeleted(Object sender, IEnumerable<OnUserEventArgs.UserIdentifier> ids)
		{
			this._userDeleted?.Invoke(sender, new OnUserEventArgs(ids));
		}

		public void EmitUserDeleted(Object sender, IEnumerable<OnUserEventArgs> events)
		{
			if (events == null) return;
			foreach (OnUserEventArgs ev in events) this._userDeleted?.Invoke(sender, ev);
		}

		#endregion

		#region User Touched

		private EventHandler<OnUserEventArgs> _userTouched;
		public event EventHandler<OnUserEventArgs> UserTouched
		{
			add { this._userTouched += value; }
			remove { this._userTouched -= value; }
		}

		public void EmitUserTouched(OnUserEventArgs.UserIdentifier id)
		{
			this.EmitUserTouched(this, new List<OnUserEventArgs.UserIdentifier>() { id });
		}

		public void EmitUserTouched(IEnumerable<OnUserEventArgs.UserIdentifier> ids)
		{
			this.EmitUserTouched(this, ids);
		}

		public void EmitUserTouched(IEnumerable<OnUserEventArgs> events)
		{
			this.EmitUserTouched(this, events);
		}

		public void EmitUserTouched(Object sender, IEnumerable<OnUserEventArgs.UserIdentifier> ids)
		{
			this._userTouched?.Invoke(sender, new OnUserEventArgs(ids));
		}

		public void EmitUserTouched(Object sender, IEnumerable<OnUserEventArgs> events)
		{
			if (events == null) return;
			foreach (OnUserEventArgs ev in events) this._userTouched?.Invoke(sender, ev);
		}

		#endregion

		#region UserProfile Deleted

		private EventHandler<OnEventArgs<Guid>> _userProfileDeleted;
		public event EventHandler<OnEventArgs<Guid>> UserProfileDeleted
		{
			add { this._userProfileDeleted += value; }
			remove { this._userProfileDeleted -= value; }
		}

		public void EmitUserProfileDeleted(Guid id)
		{
			this.EmitUserProfileDeleted(this, new List<Guid>() { id });
		}

		public void EmitUserProfileDeleted(IEnumerable<Guid> ids)
		{
			this.EmitUserProfileDeleted(this, ids);
		}

		public void EmitUserProfileDeleted(IEnumerable<OnEventArgs<Guid>> events)
		{
			this.EmitUserProfileDeleted(this, events);
		}

		public void EmitUserProfileDeleted(Object sender, IEnumerable<Guid> ids)
		{
			this._userProfileDeleted?.Invoke(sender, new OnEventArgs<Guid>(ids));
		}

		public void EmitUserProfileDeleted(Object sender, IEnumerable<OnEventArgs<Guid>> events)
		{
			if (events == null) return;
			foreach (OnEventArgs<Guid> ev in events) this._userProfileDeleted?.Invoke(sender, ev);
		}

		#endregion

		#region UserProfile Touched

		private EventHandler<OnEventArgs<Guid>> _userProfileTouched;
		public event EventHandler<OnEventArgs<Guid>> UserProfileTouched
		{
			add { this._userProfileTouched += value; }
			remove { this._userProfileTouched -= value; }
		}

		public void EmitUserProfileTouched(Guid id)
		{
			this.EmitUserProfileTouched(this, new List<Guid>() { id });
		}

		public void EmitUserProfileTouched(IEnumerable<Guid> ids)
		{
			this.EmitUserProfileTouched(this, ids);
		}

		public void EmitUserProfileTouched(IEnumerable<OnEventArgs<Guid>> events)
		{
			this.EmitUserProfileTouched(this, events);
		}

		public void EmitUserProfileTouched(Object sender, IEnumerable<Guid> ids)
		{
			this._userProfileTouched?.Invoke(sender, new OnEventArgs<Guid>(ids));
		}

		public void EmitUserProfileTouched(Object sender, IEnumerable<OnEventArgs<Guid>> events)
		{
			if (events == null) return;
			foreach (OnEventArgs<Guid> ev in events) this._userProfileTouched?.Invoke(sender, ev);
		}

		#endregion

		#region UserSettings Deleted

		private EventHandler<OnEventArgs<Guid>> _userSettingsDeleted;
		public event EventHandler<OnEventArgs<Guid>> UserSettingsDeleted
		{
			add { this._userSettingsDeleted += value; }
			remove { this._userSettingsDeleted -= value; }
		}

		public void EmitUserSettingsDeleted(Guid id)
		{
			this.EmitUserSettingsDeleted(this, new List<Guid>() { id });
		}

		public void EmitUserSettingsDeleted(IEnumerable<Guid> ids)
		{
			this.EmitUserSettingsDeleted(this, ids);
		}

		public void EmitUserSettingsDeleted(IEnumerable<OnEventArgs<Guid>> events)
		{
			this.EmitUserSettingsDeleted(this, events);
		}

		public void EmitUserSettingsDeleted(Object sender, IEnumerable<Guid> ids)
		{
			this._userSettingsDeleted?.Invoke(sender, new OnEventArgs<Guid>(ids));
		}

		public void EmitUserSettingsDeleted(Object sender, IEnumerable<OnEventArgs<Guid>> events)
		{
			if (events == null) return;
			foreach (OnEventArgs<Guid> ev in events) this._userSettingsDeleted?.Invoke(sender, ev);
		}

		#endregion

		#region UserSettings Touched

		private EventHandler<OnEventArgs<Guid>> _userSettingsTouched;
		public event EventHandler<OnEventArgs<Guid>> UserSettingsTouched
		{
			add { this._userSettingsTouched += value; }
			remove { this._userSettingsTouched -= value; }
		}

		public void EmitUserSettingsTouched(Guid id)
		{
			this.EmitUserSettingsTouched(this, new List<Guid>() { id });
		}

		public void EmitUserSettingsTouched(IEnumerable<Guid> ids)
		{
			this.EmitUserSettingsTouched(this, ids);
		}

		public void EmitUserSettingsTouched(IEnumerable<OnEventArgs<Guid>> events)
		{
			this.EmitUserSettingsTouched(this, events);
		}

		public void EmitUserSettingsTouched(Object sender, IEnumerable<Guid> ids)
		{
			this._userSettingsTouched?.Invoke(sender, new OnEventArgs<Guid>(ids));
		}

		public void EmitUserSettingsTouched(Object sender, IEnumerable<OnEventArgs<Guid>> events)
		{
			if (events == null) return;
			foreach (OnEventArgs<Guid> ev in events) this._userSettingsTouched?.Invoke(sender, ev);
		}

		#endregion

		#region Hierarchy Context Grant Touched

		private EventHandler<OnEventArgs<String>> _hierarchyContextGrantTouched;
		public event EventHandler<OnEventArgs<String>> HierarchyContextGrantTouched
		{
			add { this._hierarchyContextGrantTouched += value; }
			remove { this._hierarchyContextGrantTouched -= value; }
		}

		public void EmitHierarchyContextGrantTouched(String id)
		{
			this.EmitHierarchyContextGrantTouched(this, new List<String>() { id });
		}

		public void EmitHierarchyContextGrantTouched(IEnumerable<String> ids)
		{
			this.EmitHierarchyContextGrantTouched(this, ids);
		}

		public void EmitHierarchyContextGrantTouched(IEnumerable<OnEventArgs<String>> events)
		{
			this.EmitHierarchyContextGrantTouched(this, events);
		}

		public void EmitHierarchyContextGrantTouched(Object sender, IEnumerable<String> ids)
		{
			this._hierarchyContextGrantTouched?.Invoke(sender, new OnEventArgs<String>(ids));
		}

		public void EmitHierarchyContextGrantTouched(Object sender, IEnumerable<OnEventArgs<String>> events)
		{
			if (events == null) return;
			foreach (OnEventArgs<String> ev in events) this._hierarchyContextGrantTouched?.Invoke(sender, ev);
		}

		#endregion
	}
}
