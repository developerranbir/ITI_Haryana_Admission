(function($) {

	"use strict";

	var fullHeight = function() {

		$('.js-fullheight').css('height', $(window).height());
		$(window).resize(function(){
			$('.js-fullheight').css('height', $(window).height());
		});

	};
	fullHeight();

	$('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
        if ($('#sidebar').hasClass('active'))
        {
            
            $('#sidebarCollapse > i').addClass('fa-chevron-right');
            $('#sidebarCollapse > i').removeClass('fa-chevron-left');
        }
        else
        {
            $('#sidebarCollapse > i').addClass('fa-chevron-left');
            $('#sidebarCollapse > i').removeClass('fa-chevron-right');
            
            
        }

  });

})(jQuery);
