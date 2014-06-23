// #pragma strict

function Start () {

}

function LateUpdate () {

	// forward
	if(Input.GetKeyDown("1")){
		Camera.main.renderingPath = RenderingPath.Forward;
		//for(var fooObj : GameObject in GameObject.FindGameObjectsWithTag("PointLight"))
		//{
		//	fooObj.light.range *= .5;
		//}
	}
	// deferred
	if(Input.GetKeyDown("2")){
		Camera.main.renderingPath = RenderingPath.DeferredLighting;
		
		//for(var fooObj : GameObject in GameObject.FindGameObjectsWithTag("PointLight"))
		//{
		//	fooObj.light.range *= 2;
		//}		
		
	}
	
	if(Input.GetKeyDown("3")){
		var mainlight3 : GameObject;
		mainlight3 = GameObject.Find("01 Sun");
		mainlight3.transform.RotateAround (mainlight3.transform.position, Vector3.up, 10.0);
	}
	if(Input.GetKeyDown("4")){
		Shader.DisableKeyword("LUX_LIGHTING_CT");
		Shader.EnableKeyword("LUX_LIGHTING_BP");
		
	}
	if(Input.GetKeyDown("5")){
		Shader.EnableKeyword("LUX_LIGHTING_CT");
		Shader.DisableKeyword("LUX_LIGHTING_BP");	
	}
}