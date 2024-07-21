'use strict';
var ts_start=Date.now();
var $ = jQuery,
		isTouchDevice = navigator.userAgent.match(/(iPhone|iPod|iPad|Android|BlackBerry|Windows Phone)/);

//Calculating The Browser Scrollbar Width
var parent, child, scrollWidth, bodyWidth;

if (scrollWidth === undefined) {
  parent      = $('<div style="width: 50px; height: 50px; overflow: auto"><div/></div>').appendTo('body');
  child       = parent.children();
  scrollWidth = child.innerWidth() - child.height(99).innerWidth();
  parent.remove();
}


//Full Width Box
function fullWidthBox() {
  if ($('.full-width-box.auto-width').length) {
		var windowWidth    = $('body').outerWidth(),
				containerWidth = $('.header .container').width();
			
		$('.full-width-box.auto-width').each(function() {
			$(this)
				.css({
					left  : ( containerWidth - windowWidth) / 2,
					width : windowWidth
				})
				.addClass('loaded');
		});
  }
}



//Animations
function animations() {
  $('[data-appear-animation]').each(function() {
		var $this = $(this);
	
		if(!$('body').hasClass('no-csstransitions') && ($('body').width() + scrollWidth) > 767) {
			$this.appear(function() {
				var delay = ($this.data('appearAnimationDelay') ? $this.data('appearAnimationDelay') : 1);
		
				if(delay > 1) $this.css('animation-delay', delay + 'ms');
				
				$this.addClass('animated').addClass($this.data('appearAnimation'));
			});
		}
	});
		
	// Animation Progress Bars 
	$('[data-appear-progress-animation]').each(function() {
		var $this = $(this);
	
		$this.appear(function() {
			var delay = ($this.attr('data-appear-animation-delay') ? $this.attr('data-appear-animation-delay') : 1);
	
			if(delay > 1) $this.css('animation-delay', delay + 'ms');
			
			$this.find('.progress-bar').addClass($this.attr('data-appear-animation'));
	
			setTimeout(function() {
				$this.find('.progress-bar').animate({
					width: $this.attr('data-appear-progress-animation')
				}, 500, 'easeInCirc', function() {
					$this.find('.progress-bar').animate({
						textIndent: 10
					}, 1500, 'easeOutBounce');
				});
			}, delay);
		}, {accX: 0, accY: -50});
  });
}

//*/

//Header Fixed
function headerCustomizer() {
  var body         = $('body'),
			topHeight    = 0,
			headerHeight = 0,
			scroll       = 0,
			fixedH       = $('.fixed-header');
  
  if ($('#top-box').length) {
		topHeight = $('#top-box').outerHeight();
  }
	
  headerHeight = $('.header').outerHeight();
  
  if (!isTouchDevice) {
		scroll = topHeight;
		
		if (body.hasClass('hidden-top')) {
			scroll = 8;
		}
		
		if (body.hasClass('padding-top')) {
			scroll = topHeight + 420;
		} else if (body.hasClass('boxed')) {
			scroll = topHeight + 20;
			if (body.hasClass('fixed-header') && body.hasClass('fixed-top')) {
				scroll = 20;
			}
		}
		
		$(window).scroll(function(){
			var $this = $(this);
			
			if (body.hasClass('fixed-header')) {
				if ($this.scrollTop() >= scroll)
					body.addClass('fixed');
				else
					body.removeClass('fixed');
			}
			
			if ($this.scrollTop() >= headerHeight)
				fixedH.addClass('background-opacity');
			else
				fixedH.removeClass('background-opacity');
		});
		
		$('.hidden-top .header, .hidden-top #top-box').not('.boxed .header, .boxed #top-box').hover(function(){
			$('.hidden-top').addClass('visible-top');
		}, function(){
			$('.hidden-top').removeClass('visible-top');
		});
		
		$(window).scroll(function(){
			var $this = $(this);
			
			if ((body.hasClass('visible-top')) && ($this.scrollTop() > 0))
				body.removeClass('visible-top');
		});
  }
  
  $(window).scroll(function(){
    if ($(this).scrollTop() >= topHeight + headerHeight)
			$('.top-fixed-box').addClass('fixed');
		else
			$('.top-fixed-box').removeClass('fixed');
  });
}

//Header Menu
function menu() {
  var body    = $('body'),
			primary = '.primary';
  
  $(primary).find('.parent > a .open-sub, .megamenu .title .open-sub').remove();
  
  if ((body.width() + scrollWidth) < 992 || $('.header').hasClass('minimized-menu'))
		$(primary).find('.parent > a, .megamenu .title').append('<span class="open-sub"><span></span><span></span></span>');
  else
		$(primary).find('ul').removeAttr('style').find('li').removeClass('active');
  
  $(primary).find('.open-sub').click(function(e){
		e.preventDefault();
		
		var item = $(this).closest('li, .box');
		
		if ($(item).hasClass('active')) {
			$(item).children().last().slideUp(600);
			$(item).removeClass('active');
		} else {
			var li = $(this).closest('li, .box').parent('ul, .sub-list').children('li, .box');
			
			if ($(li).is('.active')) {
				$(li).removeClass('active').children('ul').slideUp(600);
			}
			
			$(item).children().last().slideDown(600);
			$(item).addClass('active');
			
			if (body.width() + scrollWidth > 991) {
				var maxHeight = body.height() - ($(primary).find('.navbar-nav')).offset().top - 20;
				
				$(primary).find('.navbar-nav').css({
					maxHeight : maxHeight,
					overflow  : 'auto'
				});
			}
		}
  });

  $(primary).find('.parent > a').click(function(e){
		if (((body.width() + scrollWidth) > 991) &&  (isTouchDevice)) {
			var $this = $(this);
			
			if ($this.parent().hasClass('open')) {
				$this.parent().removeClass('open')
			} else {
				e.preventDefault();
				
				$this.parent().addClass('open')
			}
		}
  });

  body.on('click', function(e) {
		if (!$(e.target).is(primary + ' *')) {
			if ($(primary + ' .collapse').hasClass('in')) {
				$(primary + ' .navbar-toggle').addClass('collapsed');
				$(primary + ' .navbar-collapse').collapse('hide');
			}
		}
  });
  
  
  
  /* Top Menu */
  var topMenu = $('.top-navbar').find('.collapse');

  if ((body.width() + scrollWidth) < 992)
		topMenu.css('width', body.find('#top-box .container').width());
	else
		topMenu.css('width', 'auto');
}

//Accordion
function accordions() {
  //Some open
  $('.multi-collapse .collapse').collapse({
		toggle: false
  });
  
  //Always open
  $('.panel a[data-toggle="collapse"]').click( function(event){
		event.preventDefault();
		
		if ($(this).closest('.panel').hasClass('active')) {
			if ($(this).closest('.panel-group').hasClass('one-open'))
				event.stopPropagation();
		}
  });

  $('.collapse').on('hide.bs.collapse', function (event) {
		event.stopPropagation();
		
		$(this).closest('.panel').removeClass('active');
  });
	
  $('.collapse').on('show.bs.collapse', function () {
		$(this).closest('.panel').addClass('active');
  });
  
  $('.collapse.in').closest('.panel').addClass('active');
}

//Tabs
function tabs() {
  var tab = $('.nav-tabs');
  
  tab.find('a').click(function (e) {
		e.preventDefault();
		
		$(this).tab('show');
  });

  if (($('body').width() + scrollWidth) < 768 && (!tab.hasClass('no-responsive'))) {
    tab.each(function(){
			var $this = $(this);
			
			if (!$this.next('.tab-content').hasClass('hidden') && !$this.find('li').hasClass('dropdown')) {
				$this.addClass('accordion-tab');
		
				$this.find('a').each(function(){
					var $this = $(this),
						id = $this.attr('href');
					
					$this.prepend('<span class="open-sub"></span>');
					
					$this.closest('.nav-tabs').next('.tab-content').find(id)
					.appendTo($this.closest('li'));
				});
				
				$this.next('.tab-content').addClass('hidden');
			}
    });
	
		$('.accordion-tab > li.active .tab-pane').slideDown();
  } else {
		tab.find('.tab-pane').removeAttr('style', 'display');
		
		tab.each(function(){
			var $this = $(this);
				
			if ($this.next('.tab-content').hasClass('hidden')) {
				$this.removeClass('accordion-tab');
				
				$this.find('a').each(function(){
					var $this = $(this),
							id = $this.attr('href');
					
					$($this.closest('li').find('.tab-pane')).appendTo($this.closest('.nav-tabs').next('.tab-content'));
				});
				
				$this.next('.tab-content').removeClass('hidden');
			}
    });
  }
  
  $('.accordion-tab > li > a').on('shown.bs.tab', function (e) {
		if (($('body').width() + scrollWidth) < 768) {	  
			var $this = $(this),
					tab = $this.closest('li');
			
			e.preventDefault();
			
			$this
				.closest('.accordion-tab')
				.find('.tab-pane').not(tab.find('.tab-pane'))
				.removeClass('active')
				.slideUp();
				
			tab.find('.tab-pane')
				.addClass('active')
				.slideDown();
	
			$('html, body').on("scroll mousedown DOMMouseScroll mousewheel keyup", function(){
				$('html, body').stop();
			});
			
			setTimeout(function(){ 
				$('html, body').animate({
					scrollTop: $this.offset().top
				}, 800);
			}, 500 );
		}
  });
}


//Footer structure (max-width < 768)
function footerStructure() {
  var footer = $('#footer .footer-bottom');
  
  if (($('body').width() + scrollWidth) < 768) {
		if (!footer.find('.new-copyright').length) {
			footer.find('.address').after('<div class="new-copyright"></div>');
			footer.find('.copyright').appendTo('#footer .footer-bottom .new-copyright');
		}
  } else {
		if (footer.find('.new-copyright').length) {
			footer.find('.copyright').prependTo('#footer .footer-bottom .row');
			footer.find('.new-copyright').remove();
		}
  }
}

//Modal Window
function centerModal() {
  $(this).css('display', 'block');
  
  var dialog = $(this).find('.modal-dialog'),
	    offset = ($(window).height() - dialog.height()) / 2;
	  
  if (offset < 10)
		offset = 10;
	
  dialog.css('margin-top', offset);
}

//Social Feed
function locationSocialFeed() {
  var socialFeed = $('.social-feed');
  
  if($.fn.isotope) {
		socialFeed.isotope({
			itemSelector: '.isotope-item',
		}).addClass('loaded');
		
		$('#load-more').click(function() {
			var item1, item2, item3, items, tmp;
			
			items = socialFeed.find('.item-clone');
			item1 = $(items[Math.floor(Math.random() * items.length)]).clone();
			item2 = $(items[Math.floor(Math.random() * items.length)]).clone();
			item3 = $(items[Math.floor(Math.random() * items.length)]).clone();
			tmp = $().add(item1).add(item2).add(item3);
		
			var images = tmp.find('img');
		
			images.imagesLoaded(function(){
				return socialFeed.isotope('insert', tmp);
			});
		});
  }
}

//Full Height Pages
function fullHeightPages() {
	var full = $('.full-height');
	
	full.removeClass('scroll');
	
	if (full.height() < $('.page-box').outerHeight()) {
		full.addClass('scroll');
	} else {
		full.removeClass('scroll');
	}
}

//Banner set Start
function bannerSetCarousel() {
  $('.banner-set .banners').each(function () {
		var $this           = $(this),
				bannerSet       = $this.closest('.banner-set'),
				prev            = bannerSet.find('.prev'),
				next            = bannerSet.find('.next'),
				autoPlay        = true,
				timeoutDuration = 3000,
				scrollDuration  = 1000,
				height;
	
		/* Animation Duration */
		if (bannerSet.data('animationDuration'))
			scrollDuration = bannerSet.data('animationDuration');
		
		/* Autoplay */
		if (bannerSet.data('autoplayDisable'))
			autoPlay = false;
		else
			/* Autoplay Speed */
			if (bannerSet.data('autoplaySpeed'))
				timeoutDuration = bannerSet.data('autoplaySpeed');
				
		$this.carouFredSel({
			auto       : {
				play : autoPlay,
				timeoutDuration : timeoutDuration
			},
			width      : '100%',
			responsive : false,
			infinite   : false,
			next       : next,
			prev       : prev,
			pagination : bannerSet.find('.pagination'),
			scroll     : {
				duration : scrollDuration,
				items : 1
			},
			onCreate: function () {
				height = $this.height();
				
				$this.find('.banner').css('height', height);
				if (bannerSet.hasClass('banner-set-mini') && bannerSet.hasClass('banner-set-no-pagination')) {
					bannerSet.find('.prev, .next').css('margin-top', -((height / 2) + 7));
				}
			}
		})
		.touchwipe({
			wipeLeft: function(){
				$this.trigger('next', 1);
			},
			wipeRight: function(){
				$this.trigger('prev', 1);
			},
			preventDefaultEvents : false
		})
		.parents('.banner-set').removeClass('load');
  });
}
//Banner set End

//Carousels Start
function carousel() {
  if ($('.carousel-box .carousel').length) {
		var carouselBox = $('.carousel-box .carousel');
	
		carouselBox.each(function () {
			var $this           = $(this),
					carousel        = $this.closest('.carousel-box'),
					prev,
					next,
					pagination,
					responsive      = false,
					autoPlay        = true,
					timeoutDuration = 1500,
					scrollDuration  = 1000;
			
			/* Animation Duration */
			if (carousel.data('animationDuration'))
				scrollDuration = carousel.data('animationDuration');
			
			/* Autoplay */
			if (carousel.data('autoplayDisable'))
				autoPlay = false;
			else
				/* Autoplay Speed */
				if (carousel.data('autoplaySpeed'))
					timeoutDuration = carousel.data('autoplaySpeed');
			
			if (carousel.data('carouselNav') != false) {
				next = carousel.find('.next');
				prev = carousel.find('.prev');
				carousel.removeClass('no-nav');
			} else {
				next = false;
				prev = false;
				carousel.addClass('no-nav');
			}
			
			if (carousel.data('carouselPagination')) {
				pagination = carousel.find('.pagination');
				carousel.removeClass('no-pagination');
			} else {
				pagination = false;
				carousel.addClass('no-pagination');
			}
			
			if (carousel.data('carouselOne'))
				responsive = true;
			
			$this.carouFredSel({
				onCreate : function () {
					$(window).on('resize', function(e){
						e.stopPropagation();
					});
				},
				scroll : {
					duration : scrollDuration,
					items : 1
				},
				auto       : {
					play : autoPlay,
					timeoutDuration : timeoutDuration
				},
				width      : '100%',
				infinite   : false,
				next       : next,
				prev       : prev,
				pagination : pagination,
				responsive : responsive
			})
			.touchwipe({
				wipeLeft: function(){
					$this.trigger('next', 1);
				},
				wipeRight: function(){
					$this.trigger('prev', 1);
				},
				preventDefaultEvents : false
			})
			.parents('.carousel-box').removeClass('load');
		});
  }
}
//Carousels End



//One Page Start
function scrollMenu() {
  var link         = $('a.scroll'),
			header       = $('.header'),
			headerHeight = header.height();
	
  if(($('body').width() + scrollWidth) < 992)
		headerHeight = 0;
  
  $(document).on('scroll', onScroll);
  
  link.on('click', function(e) {
		var target = $(this).attr('href'),
				$this = $(this);
			
		e.preventDefault();
		
		link.removeClass('active');
			$this.addClass('active');
		
		if ($(target).length) {
			$('html, body').animate({scrollTop: $(target).offset().top - headerHeight}, 600);
		}
  });
  
  function onScroll(){
    var scrollPos = $(document).scrollTop();
		
    link.each(function () {
			var currLink = $(this),
					refElement;
			
			if ($(currLink.attr('href')).length) {
				refElement = $(currLink.attr('href'));
				
				if (
				refElement.position().top - headerHeight <= scrollPos &&
				refElement.position().top + refElement.height() > scrollPos) {
					link.removeClass('active');
					currLink.addClass('active');
				} else {
					currLink.removeClass('active');
				}
			}
    });
  }
	
  $('a.scroll').on('click', function(e) {
		var header = $('.header'),
				headerHeight = header.height(),
				target = $(this).attr('href'),
				$this = $(this);
		
		e.preventDefault();
	
		if ($(target).length) {
			if(($('body').width() + scrollWidth) > 991)
				$('html, body').animate({scrollTop: $(target).offset().top - (headerHeight)}, 600);
			else
				$('html, body').animate({scrollTop: $(target).offset().top}, 600);
		}
	
		$('a.scroll').removeClass('active');
		$this.addClass('active');
  });
}
//One Page End

$(document).ready(function(){
  //Replace img > IE8
  if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)){
		var ieversion = new Number(RegExp.$1);
		
		if (ieversion < 9) {
			$('img[src*="svg"]').attr('src', function() {
				return $(this).attr('src').replace('.svg', '.png');
			});
		}
  }
  
  //IE 
  if (/MSIE (\d+\.\d+);/.test(navigator.userAgent))
		$('html').addClass('ie');

  //Touch device
  if( isTouchDevice )
		$('body').addClass('touch-device');

  //Bootstrap Elements
  $('[data-toggle="tooltip"], .tooltip-link').tooltip();
  
  $("a[data-toggle=popover]")
		.popover()
		.click(function(e) {
			e.preventDefault();
		});
  
  $('.btn-loading').click(function () {
    var btn = $(this);
	
    btn.button('loading');
	
    setTimeout(function () {
      btn.button('reset')
    }, 3000);
  });
  
  $('.disabled, fieldset[disabled] .selectBox').click(function () {
    return false;
  });

  $('.modal-center').on('show.bs.modal', centerModal);
  
	//Functions
  if(typeof fullWidthBox == 'function') fullWidthBox();
  if(typeof menu == 'function') menu();
  if(typeof footerStructure == 'function') footerStructure();
  if(typeof tabs == 'function') tabs();
  if(typeof accordions == 'function') accordions();
  if(typeof headerCustomizer == 'function') headerCustomizer();
  if(typeof modernGallery == 'function') modernGallery();
  if(typeof animations == 'function') animations();
  if(typeof formStylization == 'function') formStylization();
  if(typeof addReview == 'function') addReview();
  if ($('.fwb-paralax').length) paralax();
  //videoBg();
  if(typeof loginRegister == 'function') loginRegister();
  if(typeof loadingButton == 'function') loadingButton();
  if(typeof productLimited == 'function') productLimited();
  if(typeof wordRotate == 'function') wordRotate();
	
	//Charts
	if ($('.chart').length && typeof chart == 'function') chart();
	if (typeof graph == 'function') graph();
	
	//Zoom
	if ($('.general-img').length && typeof zoom == 'function') zoom();
	
	//Blur
	if ($('.full-width-box .fwb-blur').length && typeof blur == 'function') blur();
	if ($('.blur-page').length && typeof blurPage == 'function') blurPage();
	
	//One Page
	if (typeof scrollMenu == 'function') scrollMenu();
	
	$(window).on('load', function() {
		//locationSocialFeed();
		if ($('.full-height').length && typeof fullHeightPages == 'function') fullHeightPages();
		
		//Slider
		if ($('.progressive-slider').length && typeof progressiveSlider == 'function') progressiveSlider();
		
		//Banner set
		if ($('.banner-set').length && typeof bannerSetCarousel == 'function') bannerSetCarousel();
		
		//Сarousels
		if ($('.carousel-box .carousel').length && typeof carousel == 'function') carousel();
		
		//Thumblist
		if ($('#thumblist').length && typeof thumblist == 'function') thumblist();
		
		//Filter
		if ($('.portfolio, .filter-box').length && typeof isotopFilter == 'function') isotopFilter();

        //alert(Date.now() - ts_start);
        try{
        $('#video_bg source').attr('src','content/video/t800.mp4');
        $("#video_bg")[0].load();
        }catch(err){}
	});
	
	//Revolution Slider Start
	if ($('.tp-banner').length && $.fn.revolution) {
		var revolutionSlider = $('.tp-banner');
		
		if (revolutionSlider.closest('.rs-slider').hasClass('full-width')) {
			var body         = $('body'),
					width        = body.width(),
					topHeight    = 0,
					headerHeight = 104,
					height;
				
			if ($('#top-box').length) {
				if (body.hasClass('hidden-top'))
					topHeight = $('#top-box').outerHeight() - 32;
			}
			
			if ((body.width() + scrollWidth) >= 1200)
				height = body.height() - (topHeight + headerHeight);
			else
				height = 800;
			
			revolutionSlider.revolution({
				delay             : 6000,
				startwidth        : 1200,
				startheight       : height,
				hideThumbs        : 10,
				navigationType    : 'bullet',
				navigationArrows  : 'solo',
				navigationHAlign  : 'center',
				navigationVAlign  : 'top',
				navigationHOffset : -545,
				navigationVOffset : 30,
				hideTimerBar      : 'on'
			}).parents('.slider').removeClass('load');
		} else {
			revolutionSlider.revolution({
				delay          : 6000,
				startwidth     : 1200,
				startheight    : 500,
				hideThumbs     : 10,
				navigationType : 'none',
				onHoverStop    : 'off'
			}).parents('.slider').removeClass('load');
		}
	}
	//Revolution Slider End
	
	//Royal Slider Start
	if($.fn.royalSlider) {
		$('.royal-slider').royalSlider({
			arrowsNav             : true,
			loop                  : false,
			keyboardNavEnabled    : true,
			controlsInside        : false,
			imageScaleMode        : 'fill',
			arrowsNavAutoHide     : false,
			autoScaleSlider       : true, 
			autoScaleSliderWidth  : 960,     
			autoScaleSliderHeight : 350,
			controlNavigation     : 'bullets',
			thumbsFitInViewport   : false,
			navigateByClick       : true,
			startSlideId          : 0,
			autoPlay              : false,
			transitionType        :'move',
			globalCaption         : false,
			deeplinking           : {
				enabled : true,
				change : true,
				prefix : 'image-'
			},
			imgWidth              : 1920,
			imgHeight             : 700
		}).parents('.slider').removeClass('load');
	}
	//Royal Slider End
	
  //Bootstrap Validator
  if($.fn.bootstrapValidator) {
		$('.form-validator').bootstrapValidator({
			excluded: [':disabled', ':hidden', ':not(:visible)'],
			feedbackIcons: {
				valid: 'glyphicon glyphicon-ok',
				invalid: 'glyphicon glyphicon-remove',
				validating: 'glyphicon glyphicon-refresh'
			},
			message: 'This value is not valid',
			trigger: null
		});
  }
  
  //Header Phone & Search
  $('.phone-header > a').click(function(e){
		e.preventDefault();
    $('.btn-group').removeClass('open');
    $('.phone-active').fadeIn().addClass('open');
  });
	
  $('.search-header > a').click(function(e){
		e.preventDefault();
    $('.btn-group').removeClass('open');
    $('.search-active').fadeIn().addClass('open');
  });
  
  $('.phone-active .close, .search-active .close').click(function(e){
		e.preventDefault();
    $(this).parent().fadeOut().removeClass('open');
  });
  
  $('body').on('click', function(e) {
		var phone  = '.phone-active',
				search = '.search-active';
	
		if ((!$(e.target).is(phone + ' *')) && (!$(e.target).is('.phone-header *'))) {
			if ($(phone).hasClass('open'))
				$(phone).fadeOut().removeClass('open');
		}
		
		if ((!$(e.target).is(search + ' *')) && (!$(e.target).is('.search-header *'))) {
			if ($(search).hasClass('open'))
				$(search).fadeOut().removeClass('open');
		}
  });
  
  $('body').on('touchstart', function (e) {
		e.stopPropagation();
	
		if ($(e.target).parents('.product, .employee').length == 0)
      $('.product, .employee').removeClass('hover');
  });

  $('.product, .employee').on('touchend', function(){
		if ($(this).hasClass('hover')) {
			$(this).removeClass('hover');
		} else {
			$('.product, .employee').removeClass('hover');
			$(this).addClass('hover');
		}
  });

  //Menu > Sidebar
  $('.menu .parent:not(".active") a').next('.sub').css('display', 'none');
  $('.menu .parent a .open-sub').click(function(e){
		e.preventDefault();
	
    if ($(this).closest('.parent').hasClass('active')) {
      $(this).parent().next('.sub').slideUp(600);
      $(this).closest('.parent').removeClass('active');
    } else {
      $(this).parent().next('.sub').slideDown(600);
      $(this).closest('.parent').addClass('active');
    }
  });
  
  
  //Gallery
  if ($.fn.fancybox){
		$('.gallery-images, .lightbox').fancybox({
			nextEffect  : 'fade',
			prevEffect  : 'fade',
			openEffect  : 'fade',
			closeEffect : 'fade',
			helpers     : {
				overlay : {
					locked : false
				}
			},
			tpl         : {
			closeBtn : '<a title="Close" class="fancybox-item fancybox-close" href="javascript:;">×</a>',
			next : '<a title="Next" class="fancybox-nav fancybox-next" href="javascript:;">\n\
						<span><svg x="0" y="0" width="9px" height="16px" viewBox="0 0 9 16" enable-background="new 0 0 9 16" xml:space="preserve"><polygon fill-rule="evenodd" clip-rule="evenodd" fill="#fcfcfc" points="1,0.001 0,1.001 7,8 0,14.999 1,15.999 9,8 "/></svg></span>\n\
					</a>',
			prev : '<a title="Previous" class="fancybox-nav fancybox-prev" href="javascript:;">\n\
						<span><svg x="0" y="0" width="9px" height="16px" viewBox="0 0 9 16" enable-background="new 0 0 9 16" xml:space="preserve"><polygon fill-rule="evenodd" clip-rule="evenodd" fill="#fcfcfc" points="8,15.999 9,14.999 2,8 9,1.001 8,0.001 0,8 "/></svg></span>\n\
					</a>'
			}
		});
  }
  // Scroll to Top
  $('#footer .up').click(function() {
    $('html, body').animate({
      scrollTop: $('body').offset().top
    }, 500);
    return false;
  });
  
  // Circular Bars - Knob
  if($.fn.knob) {
		$('.knob').each(function () {
			var $this   = $(this),
					knobVal = $this.attr('rel');
	
			$this.knob({
				'draw' : function () { 
					$(this.i).val(this.cv + '%')
				}
			});
			
			$this.appear(function() {
				$({
					value: 0
				}).animate({
					value: knobVal
				}, {
					duration : 2000,
					easing   : 'swing',
					step     : function () {
						$this.val(Math.ceil(this.value)).trigger('change');
					}
				});
			}, {accX: 0, accY: -150});
		});
  }
  
  
  //JS loaded
  $('body').addClass('loaded');
  
  //Scrollbar
  if ($.fn.scrollbar)
		$('.minimized-menu .primary .navbar-nav').scrollbar();
  
  
    
});




//Window Resize
(function(){
  var delay = (function(){
		var timer = 0;
		return function(callback, ms){
			clearTimeout (timer);
			timer = setTimeout(callback, ms);
		};
  })();
  
  function resizeFunctions(){
		//Functions
		fullWidthBox();
		menu();
		footerStructure();
		tabs();
		// modernGallery();
		animations();
        //if ($('.fwb-paralax').length) paralax();
		//loginRegister();
        if ($('.full-height').length && typeof fullHeightPages == 'function') fullHeightPages();
		$('.modal-center:visible').each(centerModal);
		if ($('.progressive-slider').length && typeof progressiveSlider == 'function') progressiveSlider();
		if ($('.banner-set').length && typeof bannerSetCarousel == 'function') bannerSetCarousel();
        
		if ($('#thumblist').length && typeof thumblist == 'function') thumblist();
        if ($('.chart').length && typeof chart == 'function') chart();
		if (typeof graph == 'function') graph(true);
		if ($('.general-img').length && typeof zoom == 'function') zoom();
		if ($('.carousel-box .carousel').length && typeof carousel == 'function') carousel();
		if ($('.portfolio, .filter-box').length && typeof isotopFilter == 'function') isotopFilter();
		if ($('.full-width-box .fwb-blur').length && typeof blur == 'function') blur();
        
        
  }

	if(isTouchDevice) {
		$(window).bind('orientationchange', function(){
			delay(function(){
				resizeFunctions();
			}, 200);
		});
  } else {
		$(window).on('resize', function(){
			delay(function(){
				resizeFunctions();
			}, 500);
		});
  }
}());