
namespace Terra.Gateway.App.Authorization
{
	public static class Permission
	{
		//Authorization
		public const String LookupContextGrantOther = "LookupContextGrantOther";
		public const String LookupContextGrantGroups = "LookupContextGrantGroups";
		public const String AddUserToContextGrantGroup = "AddUserToContextGrantGroup";
		public const String RemoveUserFromContextGrantGroup = "RemoveUserFromContextGrantGroup";
		//User
		public const String BrowseUser = "BrowseUser";
		public const String BrowseUserGroup = "BrowseUserGroup";
		//Workflow
		public const String BrowseWorkflowDefinition = "BrowseWorkflowDefinition";
		public const String CanExecuteImageInference = "CanExecuteImageInference";
		public const String BrowseWorkflowExecution = "BrowseWorkflowExecution";
		public const String BrowseWorkflowTask = "BrowseWorkflowTask";
		public const String BrowseWorkflowTaskInstance = "BrowseWorkflowTaskInstance";
		public const String BrowseWorkflowXCom = "BrowseWorkflowXCom";
		public const String BrowseWorkflowTaskLog = "BrowseWorkflowTaskLog";
		public const String RerunWorkflowTask = "RerunWorkflowTask";
	}
}
