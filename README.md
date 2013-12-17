JsMvc ASP.Net
========

Using ajax even easier Asp.Net
        
1. Create  model.

2. Mark the attribute [JsMvcBaseAtribute...].

3. Object models do json send to page: 

        [WebMethod]
        public JsonResult GetJsonText()
        {
            return this.Json(UtilsCore.GetJson(new ModelIon()); 
        }
        
4. Json on the client inserted into  plugin: 

         $("#testdiv").Rendering({
                    'Model': responseJson,
                    'Table': true,
          });
          
5. Ajax to server:

  getJsonJs();
  
Do not forget for validate:
  
   Web.config section appSettings:
   
   add key="ClientValidationEnabled" value="true" 
   
   add key="UnobtrusiveJavaScriptEnabled" value="true" 
   
 
