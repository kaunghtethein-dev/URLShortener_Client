window.AppJS = {
    hideElement: function (element) {
        if (element) {
            element.classList.remove('show');
            // Wait for animation to complete before hiding completely
            setTimeout(() => {
                element.style.display = 'none';
            }, 300);
        }
    },
    showElement: function (element) {
        if (element) {
            element.style.display = 'block';
            // Trigger reflow to ensure transition works
            void element.offsetWidth;
            element.classList.add('show');
        }
    },
    toggleLoading: function (loadingStart) {
        console.log("loadingStart => ", loadingStart);
        var loader = document.getElementById("loading");
        if (loader) {
            if (loadingStart) {
                loader.style.display = "block";
            }
            else {
                loader.style.display = "none";
            }
        }
    },
    downloadFile: function (fileName, mimeType, base64Data) {
        const link = document.createElement("a");
        link.download = fileName;
        link.href = `data:${mimeType};base64,${base64Data}`;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    },
    copyToClipboard:function(text) {
        navigator.clipboard.writeText(text)
            .catch(err => {
                alert("Failed to copy: ", err);
            });
    },
    createLast7DaysChart: function (elementId, data) {
        console.log("createLast7DaysChart triggered");
        const ctx = document.getElementById(elementId);
        console.log("ctx => ", ctx);
        if (ctx) {
            const labels = data.map(item => {
                if (item.date) {
                    const date = new Date(item.date);
                    const monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                        'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];

                    const month = monthNames[date.getMonth()];
                    const day = date.getDate(); 

                    return `${month} ${day}`;
                }
                return '';
            });

            const clickData = data.map(item => item.clickCount || 0);
            new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Click Counts',
                        data: clickData,
                        backgroundColor: [
                            '#235ce5',
                        ],
                    }]
                },
                options: {
                    indexAxis: 'y',
                    x: {
                        ticks: {
                            // Ensure whole numbers on y-axis
                            callback: function (value) {
                                if (Number.isInteger(value)) {
                                    return value;
                                }
                                return '';
                            },
                            stepSize: 1 
                        },
                    }
                }
            });
        }
        
    }

};