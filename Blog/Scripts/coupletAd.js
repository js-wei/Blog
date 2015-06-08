var delta=0.05
var collection;
var closeB=false;
function floaters() {
    this.items = [];
    this.addItem = function(id,x,y,content){
       document.write('<div id='+id+' style="z-index:10;position:absolute;left:'
        +(typeof(x)=='string'?eval(x):x)+';top:'+(typeof(y)=='string'?eval(y):y)+'">'
        +'<a href="javascript:void(0);" style="font-size:14px;color:#000;position:absolute;text-decoration:none;right:4px;" onclick="closeAd();">x</a>'
        +content+'</div>');
       var newItem    = {};
       newItem.object   = document.getElementById(id);
       newItem.x    = x;
       newItem.y    = y;
       this.items[this.items.length]  = newItem;
    }
    this.play = function() {
      collection    = this.items
      setInterval('play()');
    }
  
}

function play(){
    if(screen.width<=800 || closeB){
      for(var i=0;i<collection.length;i++){
        collection[i].object.style.display  = 'none';
      }
      return;
    }
    
    for(var i=0;i<collection.length;i++){
      var followObj  = collection[i].object;
      var followObj_x  = (typeof(collection[i].x)=='string'?eval(collection[i].x):collection[i].x);
      var followObj_y  = (typeof(collection[i].y)=='string'?eval(collection[i].y):collection[i].y);

      if(followObj.offsetLeft!=(document.documentElement.scrollLeft+followObj_x)) {
         var dx=(document.documentElement.scrollLeft+followObj_x-followObj.offsetLeft)*delta;
         dx=(dx>0?1:-1)*Math.ceil(Math.abs(dx));
         followObj.style.left=followObj.offsetLeft+dx + 'px';
       }

      if(followObj.offsetTop!=(document.documentElement.scrollTop+followObj_y)) {
         var dy=(document.documentElement.scrollTop+followObj_y-followObj.offsetTop)*delta;
         dy=(dy>0?1:-1)*Math.ceil(Math.abs(dy));
         followObj.style.top=followObj.offsetTop +dy + 'px';
       }
      followObj.style.display = '';
    }
} 
function closeAd(){
   closeB=true;
   return;
}




/***
	//对联广告
	var theFloaters  = new floaters();
	theFloaters.addItem('followDiv1','document.body.clientWidth-120',230,'<a href="" target="_blank" style="display:block;"><img src="1.gif" border="0"></a>'+'text');
	theFloaters.addItem('followDiv2',20,230,'<a href="" target="_blank" style="display:block;"><img src="1.gif" border="0"></a>'+'text');
	theFloaters.play();
	//漂浮广告
	var ad=new ad();
	ad.addItem('<a href="http://sc.jb51.net" target="_blank"><img src="1.gif" width="80" height="80" border="0"></a>');
	ad.play();
***/