/*
 *****************************************************
 *	CUSTOM JS DOCUMENT                              *
 *	Single window load event                        *
 *   "use strict" mode on                            *
 *****************************************************
 */
$(window).on("load", function() {

    "use strict";


    var preLoader = $('.preloader');
    var MixItUp1 = $('#MixItUp1');
    var fancybox = $('.fancybox');
    var fancybox = $('.fancybox');
    var faqsAccordion = $('#faqs-accordion');
    var waTabBtn = $('.wa-tabs .womens_btn');
    var waTabBtn1 = $('#wa-tabs1 .mens_btn');
    var waTabContainer = $('.wa-tabs .womens_tab');
    var waTabContainer1 = $('#wa-tabs1 .mens_tab');
    var cartPopup = $('#cart');
    var minicart = $(".mini-cart-wrapper");
    var uiTrigger = $('[js-ui-menu-trigger]');
    var priceShow = $('#price_show');
    var sliderHandle = $('.ui-slider-handle');
    var itemFav = $('[js-item-fav]')
    var priceRangeFilter = $('.price-range-filter')
    var dp = $(".display-img");
    var off_tag2 = $(".off_tag2,.off_tag3");
    var bodys = $("body");
    var quickView = $(".quickView");
    var modalBackdrop = $(".modal_backdrop");
    var closeModal = $(".close-modal");
    var thumbs = $(".icon-images").find("a");
    var dp = $(".display-img");
    var jsPriceSlider = $(".js-price-slider");
	var tpBanner= jQuery('.fullwidthbanner');

    // ============================================
    // PreLoader On window Load
    // =============================================
	if (preLoader.length) {
    preLoader.addClass('loaderout');
	}

    //============================================
    // MixItUp settings
    //============================================

    if (MixItUp1.length) {
        MixItUp1.mixItUp({
            selectors: {
                filter: '.filter-1'
            }
        });
    }
	
    //=========================================
    // Tabs
    //=========================================			

    if (waTabBtn.length) {
        waTabBtn.on('click', function(e) {
            e.preventDefault();
            var target = $($(this).attr('href'));
            waTabBtn.removeClass('active-btn');
            $(this).addClass('active-btn');
            waTabContainer.fadeOut(0);
            waTabContainer.removeClass('active-tab');
            $(target).fadeIn(500);
            $(target).addClass('active-tab');
        });

    }
    if (waTabBtn1.length) {
        waTabBtn1.on('click', function(e) {
            e.preventDefault();
            var target = $($(this).attr('href'));
            waTabBtn1.removeClass('active-btn');
            $(this).addClass('active-btn');
            waTabContainer1.fadeOut(0);
            waTabContainer1.removeClass('active-tab');
            $(target).fadeIn(500);
            $(target).addClass('active-tab');
        });

    }


    //========================================
    // Accordion 
    //======================================== 	

    if (faqsAccordion.length) {
        faqsAccordion.accordion();
    }
    //========================================
    // LightBox / Fancybox
    //======================================== 	

    if (fancybox.length) {
        fancybox.fancybox();
    }

    //***************************************
    // Map initialization function Calling
    //****************************************

    initMap();

    //minicart.hide();
    cartPopup.on("click", function() {
        minicart.fadeToggle("fast").removeClass('shop_hide');
    });
	
	
    //***************************************
    // All Owl Carousel function Calling
    //****************************************

    owlCarouselInit();
	
       	/* ---------------------	
			Main Slider Calling
    /* --------------------- */
    if(tpBanner.length) {
        // Hone page one Revolution Slider Initialize			


    homePageSliderInit();

    }

    /*******************Price Range***************/
    if (priceShow.length) {

        var formatCurrency = function(num) {
            return (num == 0) ? '$' + num : '$' + num + 'k';
        }
       
        jsPriceSlider.slider({
            range: true,
            min: 0,
            max: 300,
            values: [0, 250],
            slide: function(event, ui) {
                $(ui.handle).attr('data-value', formatCurrency(ui.value));
                priceShow.html(formatCurrency(ui.values[0]) + "  -  " + formatCurrency(ui.values[1]));
            }
        });
    }

    /*****************Product Popup*****************/

    off_tag2.on('click', function() {
        bodys.addClass('modal-open');
        bodys.css("padding-right", 0);
        quickView.fadeIn(400);
        modalBackdrop.fadeIn(400);
        return false;
    });

    // CLOSE MODEL 
    closeModal.on('click', function() {
        quickView.fadeOut(400);
        bodys.removeClass('modal-open');
        modalBackdrop.fadeOut(400);
        return false;
    });

    /*****************Quick view*****************/

    thumbs.click(function(event) {
		event.preventDefault();
        var src = $(this).attr("href");
        dp.attr("src", src);
    });

}); // End of the window load event


/*******************************************
	Main Slider
*******************************************/

	function homePageSliderInit() {
	
		"use strict";
		var tpBanner= jQuery('.fullwidthbanner');
	
		tpBanner.show().revolution({
            dottedOverlay: "none",
            delay: 9000,
            startwidth: 1920,
            startheight: 700,
            hideThumbs: 200,

            thumbWidth: 100,
            thumbHeight: 50,
            thumbAmount: 2,

            simplifyAll: "off",
            navigation: {
                keyboardNavigation: "on",
                keyboard_direction: "horizontal",
                mouseScrollNavigation: "off",
                onHoverStop: "off",
                touch: {
                    touchenabled: "on",
                    swipe_threshold: 75,
                    swipe_min_touches: 1,
                    swipe_direction: "horizontal",
                    drag_block_vertical: false
                },
                arrows: {
                    style: "gyges",
                    enable: true,
                    hide_onmobile: false,
                    hide_onleave: false,
                    tmp: '',
                    left: {
                        h_align: "left",
                        v_align: "center",
                        h_offset: 10,
                        v_offset: 0
                    },
                    right: {
                        h_align: "right",
                        v_align: "center",
                        h_offset: 10,
                        v_offset: 0
                    }
                }
            },
            navigationType: "bullet",
            navigationArrows: "solo",
            navigationStyle: "preview4",

            touchenabled: "on",
            onHoverStop: "on",
            nextSlideOnWindowFocus: "off",

            swipe_threshold: 75,
            swipe_velocity: 0.7,
            swipe_min_touches: 1,
            swipe_max_touches: 1,
            drag_block_vertical: false,

            keyboardNavigation: "off",

            navigationHAlign: "center",
            navigationVAlign: "bottom",
            navigationHOffset: 0,
            navigationVOffset: 20,

            soloArrowLeftHalign: "left",
            soloArrowLeftValign: "center",
            soloArrowLeftHOffset: 20,
            soloArrowLeftVOffset: 0,

            soloArrowRightHalign: "right",
            soloArrowRightValign: "center",
            soloArrowRightHOffset: 20,
            soloArrowRightVOffset: 0,

            shadow: 0,
            fullWidth: "on",
            fullScreen: "off",

            spinner: "spinner0",

            stopLoop: "off",
            stopAfterLoops: -1,
            stopAtSlide: -1,

            hideTimerBar: "on",

            shuffle: "off",

            autoHeight: "off",
            forceFullWidth: "off",

            hideThumbsOnMobile: "off",
            hideNavDelayOnMobile: 1500,
            hideBulletsOnMobile: "off",
            hideArrowsOnMobile: "off",
            hideThumbsUnderResolution: 0,

            hideSliderAtLimit: 0,
            hideCaptionAtLimit: 0,
            hideAllCaptionAtLilmit: 0,
            startWithSlide: 0,
            videoJsPath: "rs-plugin/videojs/",
            fullScreenOffsetContainer: ""
        });
	}

//***************************************
// Contact Page Map
//****************************************  

	function initMap() {
		"use strict";

		var mapDiv = $('#gmap_canvas');

		if (mapDiv.length) {
			var myOptions = {
				zoom: 10,
				center: new google.maps.LatLng(-37.81614570000001, 144.95570680000003),
				mapTypeId: google.maps.MapTypeId.ROADMAP
			};
			var map = new google.maps.Map(document.getElementById('gmap_canvas'), myOptions);
			var marker = new google.maps.Marker({
				map: map,
				position: new google.maps.LatLng(-37.81614570000001, 144.95570680000003)
			});
			var infowindow = new google.maps.InfoWindow({
				content: '<strong>Envato</strong><br>Envato, King Street, Melbourne, Victoria<br>'
			});
			google.maps.event.addListener(marker, 'click', function() {
				infowindow.open(map, marker);
			});

			infowindow.open(map, marker);
		}

	}

/* ---------------------	
	All owl Carousels 
/* --------------------- */
	function owlCarouselInit() {

    "use strict";

    //==========================================
    // owl carousels settings
    //===========================================

    var home1MainSlider = $('#home1-main-slider');
    var testimonialSection1 = $('#testimonial-section1');
    var testimonialSection2 = $('#testimonial-section2');
    var testimonialSidebar = $('#testimonial-sidebar');
    var waProductSidebar = $('#wa-product-sidebar');
    var serviceCarousel1 = $('#service-carousel1');
    var processCarousel = $('#process-carousel');
    var blogCarousel = $("#blog-carousel");
    var womenCarousel = $("#women-carousel");
    var womenLatest = $("#women-latest");
    var womenBest = $("#women-best");
    var womenPopular = $("#women-popular");
    var menCarousel = $("#men-carousel");
    var menLatest = $("#men-latest");
    var menBest = $("#men-best");
    var menPopular = $("#men-popular");
    var womenSingleCarousel = $("#women-single-carousel");
    var womenSingleCarousel1 = $("#women-single-carousel1");
    var homeBlogCarousel = $("#home-blog-carousel");
    var waPartnerCarousel = $('.wa-partner-carousel');


    if (home1MainSlider.length) {
        home1MainSlider.owlCarousel({
            autoPlay: true,
            items: 1,
            singleItem: true,
            navigation: true,
            pagination: true,

        });
    }

    if (testimonialSection1.length) {
        testimonialSection1.owlCarousel({
            autoPlay: true,
            items: 1,
            singleItem: true,
            navigation: true,
            pagination: false,

        });
    }

    if (testimonialSection2.length) {
        testimonialSection2.owlCarousel({
            autoPlay: false,
            items: 3,
            navigation: true,
            pagination: false,
            temsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3]

        });
    }

    if (testimonialSidebar.length) {
        testimonialSidebar.owlCarousel({
            autoPlay: true,
            items: 1,
            singleItem: true,
            navigation: false,
            pagination: true,

        });
    }

    if (waProductSidebar.length) {
        waProductSidebar.owlCarousel({
            autoPlay: true,
            items: 1,
            singleItem: true,
            navigation: false,
            pagination: true,

        });
    }

    if (serviceCarousel1.length) {
        serviceCarousel1.owlCarousel({
            autoPlay: false,
            items: 3,
            navigation: true,
            pagination: false,
            temsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3]

        });
    }

    if (processCarousel.length) {
        processCarousel.owlCarousel({
            autoPlay: false,
            items: 3,
            navigation: true,
            pagination: false,
            temsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3]

        });
    }

    if (blogCarousel.length) {
        blogCarousel.owlCarousel({
            autoPlay: false,
            items: 3,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3],
            navigation: true,
            pagination: false

        });
    }
    if (womenCarousel.length) {
        womenCarousel.owlCarousel({
            autoPlay: false,
            items: 4,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3],
            navigation: true,
            pagination: false

        });
    }
    if (womenLatest.length) {
        womenLatest.owlCarousel({
            autoPlay: false,
            items: 4,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3],
            navigation: true,
            pagination: false

        });
    }
    if (womenBest.length) {
        womenBest.owlCarousel({
            autoPlay: false,
            items: 4,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3],
            navigation: true,
            pagination: false

        });
    }
    if (womenPopular.length) {
        womenPopular.owlCarousel({
            autoPlay: false,
            items: 4,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3],
            navigation: true,
            pagination: false

        });
    }
    if (menCarousel.length) {
        menCarousel.owlCarousel({
            autoPlay: false,
            items: 4,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3],
            navigation: true,
            pagination: false

        });
    }
    if (menLatest.length) {
        menLatest.owlCarousel({
            autoPlay: false,
            items: 4,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3],
            navigation: true,
            pagination: false

        });
    }
    if (menBest.length) {
        menBest.owlCarousel({
            autoPlay: false,
            items: 4,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3],
            navigation: true,
            pagination: false

        });
    }
    if (menPopular.length) {
        menPopular.owlCarousel({
            autoPlay: false,
            items: 4,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3],
            navigation: true,
            pagination: false

        });
    }
    if (womenSingleCarousel.length) {
        womenSingleCarousel.owlCarousel({
            autoPlay: false,
            items: 3,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3],
            navigation: true,
            pagination: false

        });
    }
    if (womenSingleCarousel1.length) {
        womenSingleCarousel1.owlCarousel({
            autoPlay: false,
            items: 4,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3],
            navigation: true,
            pagination: false

        });
    }
    if (homeBlogCarousel.length) {
        homeBlogCarousel.owlCarousel({
            autoPlay: false,
            items: 3,
            navigation: true,
            pagination: false,
            itemsDesktop: [1199, 3],
            itemsDesktopSmall: [979, 3]

        });
    }

    if (waPartnerCarousel.length) {
        waPartnerCarousel.owlCarousel({
            autoPlay: false,
            items: 4,
            itemsDesktop: [1199, 4],
            itemsDesktopSmall: [979, 3],
            margin: 5,
            navigation: true,
            pagination: false

        });
    }
	}