/*!
 * jQuery jCombo Plugin
 * Carlos De Oliveira
 * cardeol@gmail.com
 *
 * Latest Release: Sep 2011 
 */
(function($) {
	$.fn.jCombo = function(url, user_options) {
		var default_options = {
				parent: "", 
				selected_value : "0",
				parent_value : "",
				initial_text: "-- Please Select --"
		};				
		var user_options = $.extend( default_options, user_options) ;
		var obj = $(this);
		if(user_options.parent!="") {
			var $parent = $(user_options.parent);			
			$parent.removeAttr("disabled","disabled");
			$parent.bind('change',  function(e) {
				obj.attr("disabled","disabled");
				if($(this).val()!="0" && $(this).val()!="") obj.removeAttr("disabled");
				__fill(	obj,
						url,
						$(this).val(),
						user_options.initial_text,
						user_options.selected_value);				
			});
		} 
		__fill(obj,url,user_options.parent_value,user_options.initial_text,user_options.selected_value);					
		function __fill($obj,$url,$id,$initext,$inival) {			
			$.ajax({
				type: "GET",
				dataType:"json",					
				url: $url + $id,
				success: function(j){
					var choices = '';
					if (j.length == 0) {
						choices += '<option value="0"></option>';
						$obj.html(choices);
					} else {
						if($initext!="" && $initext!=null) { 
							choices += '<option value="0">' + $initext + '</option>';
						}
			for (var i = 0; i < j.length; i++) {
			    c = j[i];
							selected = (c.Id==$inival)?' selected="selected"':'';
							
							choices += '<option value="' + c.Id + '"' + 
							selected + '>' + c.Name + 
							'</option>';
						}
						$obj.html(choices);
					}           										
					$obj.trigger("change");
				}
			});					
		}
	}
})(jQuery);