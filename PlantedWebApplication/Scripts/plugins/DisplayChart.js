function LoadPriceData(){
    let data = [];

    jsonVariableWithPrices.forEach(function(object){
        data.push([moment(object.PriceDate).format('YYYY-MM-DD'),object.Price]);
    });

    let plot1 = $.jqplot('discogsChart', [data], {
        height: 600,
        width: 600,
        title: 'Price Fluctuation',
        series: [
            {label: ' Price Date'}
        ],
        legend: {
            show: true,
            placement: 'outsideGrid'
        },
        axes: {
            xaxis: {
                label: 'Date',
                renderer: $.jqplot.DateAxisRenderer,
                tickOptions: {formatString: '%F'},
                tickInterval: '7 day'
            },
            yaxis: {
                label: 'EUR',
                min: 0,
                max: 250,
                tickInterval: 50
            }
        },
    });
}