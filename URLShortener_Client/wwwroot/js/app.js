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
    }

};