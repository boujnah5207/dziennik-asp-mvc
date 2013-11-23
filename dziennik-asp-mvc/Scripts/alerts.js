//<![CDATA[
$(document).ready(function() {    
	$(".close").click(function () {
	        $(".alert").fadeOut("slow");
	});
		
    setTimeout(function(){
        $(".alert").fadeOut("slow");
    },3000);
});
// ]]>
