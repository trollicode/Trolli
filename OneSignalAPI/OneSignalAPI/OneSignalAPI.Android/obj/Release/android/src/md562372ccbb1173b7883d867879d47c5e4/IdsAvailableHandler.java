package md562372ccbb1173b7883d867879d47c5e4;


public class IdsAvailableHandler
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.onesignal.OneSignal.IdsAvailableHandler
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_idsAvailable:(Ljava/lang/String;Ljava/lang/String;)V:GetIdsAvailable_Ljava_lang_String_Ljava_lang_String_Handler:Com.OneSignal.Android.OneSignal/IIdsAvailableHandlerInvoker, OneSignal.Android.Binding\n" +
			"";
		mono.android.Runtime.register ("Com.OneSignal.IdsAvailableHandler, Com.OneSignal, Version=3.2.0.0, Culture=neutral, PublicKeyToken=null", IdsAvailableHandler.class, __md_methods);
	}


	public IdsAvailableHandler ()
	{
		super ();
		if (getClass () == IdsAvailableHandler.class)
			mono.android.TypeManager.Activate ("Com.OneSignal.IdsAvailableHandler, Com.OneSignal, Version=3.2.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void idsAvailable (java.lang.String p0, java.lang.String p1)
	{
		n_idsAvailable (p0, p1);
	}

	private native void n_idsAvailable (java.lang.String p0, java.lang.String p1);

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
