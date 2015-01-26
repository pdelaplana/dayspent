/**
 * binding handler to create a doughnut chart using chartjs
 * 
 */
ko.bindingHandlers.doughnutChart = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        this.originalHeight = element.height;
        this.originalWidth = element.width;

    },
	update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
	    var bindings = allBindings(),
	        data = ko.utils.unwrapObservable(valueAccessor()),
	        displayText = bindings.displayText == null ? "" : bindings.displayText;
	    
	    if (this.chart) {
	        this.chart.destroy();
	        delete this.chart;
	    }
	    

	    // Get the context of the canvas element we want to select
	    var ctx = element.getContext('2d');
		
		this.chart = new Chart(ctx).Doughnut(data, {
			//animation:true,
		    responsive: false,
		    maintainAspectRatio:true,
			showTooltips: false,
			percentageInnerCutout: 80,
			//segmentShowStroke : false,
			onAnimationComplete: function () {
				var canvasWidthvar = $(element).width();
				var canvasHeight = $(element).height();
				var constant = 110;
				var fontsize = (canvasHeight / constant).toFixed(2);
				//ctx.font="2.8em Verdana";
				ctx.font = fontsize + "em Verdana";
				ctx.textBaseline = "middle";
				var total = 0;
				$.each(data, function () {
					total += parseInt(this.value, 10);
				});
				//var tpercentage = ((data[0].value / total) * 100).toFixed(2) + "%";
				//var textWidth = ctx.measureText(tpercentage).width;
				//var displayText = app.utils.formatTime(timeSpentInSecs / 60)
				var textWidth = ctx.measureText(displayText).width;

				var txtPosx = Math.round((canvasWidthvar - textWidth) / 2);
				ctx.fillText(displayText, txtPosx, canvasHeight / 2);

			}
		});

		element.height = this.originalHeight;
		element.width = this.originalWidth;
		
	}
};
