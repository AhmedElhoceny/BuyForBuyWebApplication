am5.ready(function() {

    // Create root element
    // https://www.amcharts.com/docs/v5/getting-started/#Root_element
    var root = am5.Root.new("chartdiv");
    
    
    // Set themes
    // https://www.amcharts.com/docs/v5/concepts/themes/
    root.setThemes([
      am5themes_Animated.new(root)
    ]);
    
    
    // Create chart
    // https://www.amcharts.com/docs/v5/charts/xy-chart/
    var chart = root.container.children.push(am5xy.XYChart.new(root, {
      panX: true,
      panY: true,
      wheelX: "panX",
      wheelY: "zoomX"
    }));
    
    // Add cursor
    // https://www.amcharts.com/docs/v5/charts/xy-chart/cursor/
    var cursor = chart.set("cursor", am5xy.XYCursor.new(root, {}));
    cursor.lineY.set("visible", false);
    
    
    // Create axes
    // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
    var xRenderer = am5xy.AxisRendererX.new(root, { minGridDistance: 30 });
    xRenderer.labels.template.setAll({
      rotation: -90,
      centerY: am5.p50,
      centerX: am5.p100,
      paddingRight: 15
    });
    
    var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
      maxDeviation: 0.3,
      categoryField: "country",
      renderer: xRenderer,
      tooltip: am5.Tooltip.new(root, {})
    }));
    
    var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
      maxDeviation: 0.3,
      renderer: am5xy.AxisRendererY.new(root, {})
    }));
    
    
    // Create series
    // https://www.amcharts.com/docs/v5/charts/xy-chart/series/
    var series = chart.series.push(am5xy.ColumnSeries.new(root, {
      name: "Series 1",
      xAxis: xAxis,
      yAxis: yAxis,
      valueYField: "value",
      sequencedInterpolation: true,
      categoryXField: "country",
      tooltip: am5.Tooltip.new(root, {
        labelText:"{valueY}"
      })
    }));
    
    series.columns.template.setAll({ cornerRadiusTL: 5, cornerRadiusTR: 5 });
    series.columns.template.adapters.add("fill", (fill, target) => {
      return chart.get("colors").getIndex(series.columns.indexOf(target));
    });
    
    series.columns.template.adapters.add("stroke", (stroke, target) => {
      return chart.get("colors").getIndex(series.columns.indexOf(target));
    });

    let Egypt = parseInt(document.getElementById("Egypt").value);
    let USA = parseInt(document.getElementById("USA").value);
    let China = parseInt(document.getElementById("China").value);
    let Japan = parseInt(document.getElementById("Japan").value);
    let Germany = parseInt(document.getElementById("Germany").value);
    let UK = parseInt(document.getElementById("UK").value);
    let France = parseInt(document.getElementById("France").value);
    let India = parseInt(document.getElementById("India").value);
    let Spain = parseInt(document.getElementById("Spain").value);
    let Russia = parseInt(document.getElementById("Russia").value);
    let Sudia = parseInt(document.getElementById("Sudia").value);
    let Emarate = parseInt(document.getElementById("Emarate").value);
    

    // Set data
    var data = [{
        country: "Egypt",
        value: Egypt
    }, {
        country: "USA",
        value: USA
    }, {
        country: "China",
        value: China
    }, {
        country: "Japan",
        value: Japan
    }, {
        country: "Germany",
        value: Germany
    }, {
        country: "UK",
        value: UK
    }, {
        country: "France",
        value: France
    }, {
        country: "India",
        value: India
    }, {
        country: "Spain",
        value: Spain
    }, {
        country: "Russia",
        value: Russia
    }, {
        country: "Sudia",
        value: Sudia
    }, {
        country: "Emarate",
        value: Emarate
    }];

    xAxis.data.setAll(data);
    series.data.setAll(data);
    
    
    // Make stuff animate on load
    // https://www.amcharts.com/docs/v5/concepts/animations/
    series.appear(1000);
    chart.appear(1000, 100);
    
});

let ShowOptions = (element) => {
    document.querySelector(".OptionsMenu").style.top = "17%";
    document.querySelector("#CheckedElementAction").value = element.querySelector(".Producttitle").innerText;
    console.log(document.querySelector("#CheckedElementAction").value)
    
}
let CancelMenu = () => {
    document.querySelector(".OptionsMenu").style.top = "-100%";
}
let DeleteProduct = () => {
    let DeletedElement = document.querySelector("#CheckedElementAction").value;
    let UserName = document.getElementById("UserName").value;
    window.location.href = "/User/DeleteProduct?ProductTitle=" + DeletedElement + "&UserName=" + UserName
}


