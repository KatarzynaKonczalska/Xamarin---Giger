package md55ac304e6d5746515e70d95b891026f9e;


public class aContentUser
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onCreateOptionsMenu:(Landroid/view/Menu;)Z:GetOnCreateOptionsMenu_Landroid_view_Menu_Handler\n" +
			"n_onOptionsItemSelected:(Landroid/view/MenuItem;)Z:GetOnOptionsItemSelected_Landroid_view_MenuItem_Handler\n" +
			"n_onBackPressed:()V:GetOnBackPressedHandler\n" +
			"n_onSaveInstanceState:(Landroid/os/Bundle;Landroid/os/PersistableBundle;)V:GetOnSaveInstanceState_Landroid_os_Bundle_Landroid_os_PersistableBundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Giger.Activities.aContentUser, Giger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", aContentUser.class, __md_methods);
	}


	public aContentUser () throws java.lang.Throwable
	{
		super ();
		if (getClass () == aContentUser.class)
			mono.android.TypeManager.Activate ("Giger.Activities.aContentUser, Giger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public boolean onCreateOptionsMenu (android.view.Menu p0)
	{
		return n_onCreateOptionsMenu (p0);
	}

	private native boolean n_onCreateOptionsMenu (android.view.Menu p0);


	public boolean onOptionsItemSelected (android.view.MenuItem p0)
	{
		return n_onOptionsItemSelected (p0);
	}

	private native boolean n_onOptionsItemSelected (android.view.MenuItem p0);


	public void onBackPressed ()
	{
		n_onBackPressed ();
	}

	private native void n_onBackPressed ();


	public void onSaveInstanceState (android.os.Bundle p0, android.os.PersistableBundle p1)
	{
		n_onSaveInstanceState (p0, p1);
	}

	private native void n_onSaveInstanceState (android.os.Bundle p0, android.os.PersistableBundle p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
