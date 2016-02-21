function OnCollisionEnter (col : Collision) { 
 if(col.gameObject.tag == "Item"){ 
 Destroy(this.gameObject); 
 }
 }