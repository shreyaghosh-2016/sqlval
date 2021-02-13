

// demo for freewall layout;
// created by Minh Nguyen;
(function($) {
	
	var setting = {}, wall;
	
	var colour = [
		"#16A085",
		"#27AE60",
		"#2980B9",
		"#8E44AD",
		"#2C3E50",
		"#F39C12",
		"#D35400",
		"#C0392B",
		"#870000"
	];

	var func = {
		preload: function() {
			var images = []
			for (var i = 1 ; i < 50 ; ++i) {
				images[i] = new Image();
				images[i].src = "example/i/photo/" + i + ".jpg";
				images[i].onload = function() {
					//window["console"] && console.log(this.src);
				}
			}
		},
		color: function(value) {
			$(".free-wall .brick").each(function() {
				var backgroundColor = colour[colour.length * Math.random() << 0];
				var bricks = $(this).find(".nested");
				!bricks.length && (bricks = $(this));
				bricks.css({
					backgroundColor: backgroundColor
				});
				bricks.attr("data-bgcolor", backgroundColor);
			});
		},
		layout: function() {
			var lwidth = $(window).width();
			wall = new freewall('.free-wall');
			
			wall.reset({
				selector: '> div',
				animate: true,
				cellW: 160,
				cellH: 160,
				onResize: function() {
					var cwidth = wall.container.width();
					wall.container.find('.full-width')
					.each(function(index, item){
						wall.fixSize({
							block: item,
							width: cwidth
						});
					});
					wall.fitWidth();
				},
				onComplete: function(lastItem, lastBlock, setting) {
					wall.container.find(".example-draggable").removeAttr("data-position");
				}
			});
			wall.fitWidth();
			$(window).trigger("resize");

			wall.container.find(".brick").each(function() {
				var $item = $(this);
				$item.attr({
					"data-class": $item.attr("class"),
					"data-style": $item.attr("style")
				});
			});

			wall.container.find(".destroy").click(function() {
				wall.destroy();
			});

			window["console"] && console.log(wall);
			// for responsive demo;
			$(".reponsive-block li>a").click(function() {
				var viewWidth = $(window).width();
				var preWidth = $(this).data("width");
				
				if (preWidth != "auto" && (preWidth - viewWidth) > 0) {
					alert('The screen\'s width not enought to test this size');
					return;
				}

				$(".reponsive-block li>a").removeClass("active");
				var preWidth = $(this).data("width");
				var margin = "10px auto";
				preWidth == "auto" && (margin = "10px");
				wall.container.css({
					margin: margin,
					width: preWidth
				});

				$(this).addClass("active");
				var cwidth = wall.container.width();
				wall.container.find('.full-width')
				.each(function(index, item){
					wall.fixSize({
						block: item,
						width: cwidth * 0.88
					});
				});
				wall.fitWidth();
			});

			if ("onhashchange" in window) {
			    window.onhashchange = function () {
			        hashChanged(window.location.hash);
			    }
			} else {
			    var storedHash = window.location.hash;
			   	setInterval(function () {
			        if (window.location.hash != storedHash) {
			            storedHash = window.location.hash;
			            hashChanged(storedHash);
			        }
			    }, 100);
			}

			function hashChanged(hash) {
				if (!hash || hash == "#") {
					wall.container.find(".brick").each(function() {
						var $item = $(this).removeAttr("style");

						$item.removeAttr("data-width");
						$item.removeAttr("data-height");
						
						$item.attr({
							"class": $item.attr("data-class")
						});

						$item.css({
							backgroundColor: $item.attr("data-bgcolor")
						});
					});
					
					wall.unFilter();
					$(".back-button").hide();
					$(".free-wall-logo").show();
				} else {
					$(hash).trigger("click");
					$(".back-button").show();
					$(".free-wall-logo").hide();
				}
				$(".header")[0].scrollIntoView(true);
			}

			// for back-button;
			$(".back-button .back-icon").click(function() {
				window.location.hash = "";
			});
		},
		drillhole: function() {
			var wall = new freewall('.free-wall-logo');
			//var temp = "<div class='cell' style='width:{width}px; height: {height}px; background-color: {color}'><div class='cover'></div></div>";
			//modified by Nilavra
			var temp = "<div class='cell' style='width:{width}px; height: {height}px; '><div class='cover'></div></div>";
			var colour = [
			        //color change of bricks :\ 
			        
				"#4279BD",
				"#4279BD"
			];

			var w = 1, h = 1, html = '', color = '', limitItem = 820;
			for (var i = 0; i < limitItem; ++i) {
				h = 1;
				w = 2 + 4 * Math.random() << 0;
				color = colour[colour.length * Math.random() << 0];
				html += temp.replace(/\{height\}/g, h*20).replace(/\{width\}/g, w*15).replace("{color}", color);
			}
			wall.container.html(html);
			wall.reset({
				selector: '.cell',
				animate: true,
				cellW: 15,
				cellH: 20,
				gutterX: 2,
				gutterY: 2,
				delay: 3,
				onResize: function() {
					this.refresh();
					var totalCol = this.container.attr('data-total-col');
					var offsetLeft = Math.round(totalCol/2 - 31);
					this.setHoles(getPosition(offsetLeft));
					this.refresh();
				},
				onBlockActive: function(block, setting) {
					// check for showing brick;
					if (block != null) {
						$(this).css({
							top: - 10000,
							left: block.left
						});
					} else {
						$(this).css({
							opacity: 0
						})
					}
				},
				onComplete: function() {
					wall.reset({
						delay: 0,
						animate: false,
						onBlockActive: function() {}
					})
				}
			});

			function getPosition(offsetLeft) {
				return [
					// F
					{
						top: 3,
						left: offsetLeft,
						width: 10,
						height: 2
					},
					{
						top: 5,
						left: offsetLeft,
						width: 2,
						height: 6
					},
					{
						top: 9,
						left: offsetLeft,
						width: 8,
						height: 2
					},
					{
						top: 9,
						left: offsetLeft+8,
						width: 2,
						height: 7
					},
					{
						top: 15,
						left: offsetLeft,
						width: 10,
						height: 2
					},
					//q
					{
						top: 9,
						left: offsetLeft+12,
						width: 10,
						height: 1
					},
					{
						top: 9,
						left: offsetLeft+12,
						width: 2,
						height: 7
					},
					{
						top: 9,
						left: offsetLeft+21,
						width: 2,
						height: 7
					},
					{
						top: 16,
						left: offsetLeft+12,
						width: 10,
						height: 1
					},
					{
						top: 15,
						left: offsetLeft+19,
						width: 2,
						height: 1
					},
					{
						top: 16,
						left: offsetLeft+21,
						width: 2,
						height: 1
					},
					{
						top: 17,
						left: offsetLeft+23,
						width: 2,
						height: 1
					},
					{
						top: 18,
						left: offsetLeft+25,
						width: 2,
						height: 1
					},
					//l 
					{
						top: 5,
						left: offsetLeft+27,
						width: 2,
						height: 12
					},
					//v
					
					
					{
						top: 5,
						left: offsetLeft+32,
						width: 2,
						height: 10
					}  ,
					{
						top: 15,
						left: offsetLeft+33,
						width: 1.5,
						height: 0.5
					},
					{
						top: 16,
						left: offsetLeft+34,
						width: 1.5,
						height: 0.5
					},
					{
						top: 17,
						left: offsetLeft+35,
						width: 2.5,
						height: 0.5
					},
					/*{
						top: 18,
						left: offsetLeft+36.5,
						width: 6,
						height: 0.5
					},*/
					
					// again opposite direction :)
					{
						top: 5,
						left: offsetLeft+39,
						width: 2,
						height: 10
					},
					
					{
						top: 16,
						left: offsetLeft+37,
						width: 1.5,
						height: 0.5
					},
					{
						top: 15,
						left: offsetLeft+38,
						width: 1.5,
						height: 0.5
					},
					
					
					
					
					
					
					
					
					
					//a
					{
						top: 7,
						left: offsetLeft +46,
						width: 3,
						height: 2
					},
					{
						top: 7,
						left: offsetLeft +49,
						width: 2,
						height: 10
					},
					{
						top: 11,
						left: offsetLeft + 45,
						width: 4,
						height: 2
					},
					{
						top: 13,
						left: offsetLeft + 45,
						width: 2,
						height: 2
					},
					{
						top: 15,
						left: offsetLeft + 45,
						width: 4,
						height: 2
					},
					//l
					
					{
						top: 5,
						left: offsetLeft+54,
						width: 2,
						height: 12
					}
					
					
					
				];
			}
			wall.fitZone();
			var totalCol = wall.container.attr('data-total-col');
			var offsetLeft = Math.round(totalCol/2 - 31);
			wall.setHoles(getPosition(offsetLeft));
			wall.refresh();
		},
		options: function() {
			$('.free-wall .options').click(function() {
				var cwidth = wall.container.width() - 10;
				var dna = $(this);
				if (!dna.hasClass('full-width')) {
					dna.addClass('full-width');
					wall.fixSize({
						block: dna,
						width: cwidth,
						height: 2000
					});
					wall.filter('.options');
					window.location.hash = "options";
				}
			});
		},
		events: function() {
			$('.free-wall .events').click(function() {
				var cwidth = wall.container.width() - 10;
				var dna = $(this);
				if (!dna.hasClass('full-width')) {
					dna.addClass('full-width');
					wall.fixSize({
						block: dna,
						width: cwidth,
						height: 720
					});
					wall.filter('.events');
					window.location.hash = "events";
				}
			});
		},
		methods: function() {
			$('.free-wall .methods').click(function() {
				var cwidth = wall.container.width() - 10;
				var dna = $(this);
				if (!dna.hasClass('full-width')) {
					dna.addClass('full-width');
					wall.fixSize({
						block: dna,
						width: cwidth,
						height: 2100
					});
					wall.filter('.methods');
					window.location.hash = "methods";
				}
			});
		},
		share: function() {
			var leftPosition, topPosition, width = 500, height = 300;
				leftPosition = (window.screen.width / 2) - ((width / 2) + 10);
				topPosition = (window.screen.height / 2) - ((height / 2) + 50);
			var windowFeatures = "status=no, height=" + height + ", width=" + width + ", resizable=yes, left=" + leftPosition + ", top=" + topPosition + ", screenX=" + leftPosition + ", screenY=" + topPosition + ", toolbar=no, menubar=no, scrollbars=no, location=no, directories=no";
			
			$(".free-wall .facebook-share").click(function() {
				window.open('http://www.facebook.com/sharer.php?u=' + encodeURIComponent(location.href), 'sharer', windowFeatures);
			});

			$(".free-wall .google-share").click(function() {
				window.open('https://plus.google.com/share?url=' + encodeURIComponent(location.href), 'sharer', windowFeatures);
			});

			$(".free-wall .twitter-share").click(function() {
				window.open("https://twitter.com/intent/tweet?original_referer=''&url=''&text=" + encodeURIComponent(location.href) , '_blank', windowFeatures);
			});
		},
		finish: function() {
			if (window.location.hash && $(window.location.hash).length) {
				$(window.location.hash).trigger("click");
				$('.back-button').show();
				$(".free-wall-logo").hide();
			} 
		}
	};
	
	window.app = {
		config: function(key, data) {
			setting[key] = data;
		},
		setup: function(options) {
			for (var i in options) {
				if (options.hasOwnProperty(i)) {
					func[i](options[i]);
				}
			}
			func['finish']();
		}
	};


})(window.Zepto || window.jQuery);
