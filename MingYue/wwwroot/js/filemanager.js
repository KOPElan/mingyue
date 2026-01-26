// File Manager JavaScript utilities

window.downloadFile = function (filename, base64Data) {
    try {
        // Convert base64 to byte array
        const byteCharacters = atob(base64Data);
        const byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
        
        // Create blob and download
        const blob = new Blob([byteArray], { type: 'application/octet-stream' });
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = filename;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        window.URL.revokeObjectURL(url);
    } catch (error) {
        console.error('Error downloading file:', error);
    }
};

// File drag and drop support (optional enhancement for future)
window.initializeDropZone = function (elementId, dotNetHelper) {
    const dropZone = document.getElementById(elementId);
    if (!dropZone) return;

    dropZone.addEventListener('dragover', (e) => {
        e.preventDefault();
        e.stopPropagation();
        dropZone.classList.add('drag-over');
    });

    dropZone.addEventListener('dragleave', (e) => {
        e.preventDefault();
        e.stopPropagation();
        dropZone.classList.remove('drag-over');
    });

    dropZone.addEventListener('drop', async (e) => {
        e.preventDefault();
        e.stopPropagation();
        dropZone.classList.remove('drag-over');

        const files = Array.from(e.dataTransfer.files);
        if (files.length > 0) {
            await dotNetHelper.invokeMethodAsync('OnFilesDropped', files.length);
        }
    });
};

// Initialize Dropzone for chunked file upload
window.initializeChunkedDropzone = function (elementId, uploadUrl, currentPath, dotNetHelper, chunkSize = 1048576) {
    // Check if Dropzone is loaded
    if (typeof Dropzone === 'undefined') {
        console.error('Dropzone library is not loaded. Please ensure the CDN script is included.');
        return null;
    }
    
    const element = document.getElementById(elementId);
    if (!element) {
        console.error('Dropzone element not found:', elementId);
        return null;
    }

    // Destroy existing dropzone if any
    if (element.dropzone) {
        element.dropzone.destroy();
    }

    const dropzone = new Dropzone(element, {
        url: uploadUrl,
        method: 'post',
        chunking: true,
        forceChunking: true,
        chunkSize: chunkSize, // uses chunkSize parameter (default 1MB)
        parallelChunkUploads: false,
        retryChunks: true,
        retryChunksLimit: 3,
        autoProcessQueue: false,
        uploadMultiple: false,
        parallelUploads: 1,
        maxFilesize: 10240, // 10GB max
        timeout: 300000, // 5 minutes
        clickable: true,
        previewsContainer: element,
        dictDefaultMessage: "Drop files here or click to upload",
        dictFallbackMessage: "Your browser doesn't support drag and drop file uploads.",
        dictFileTooBig: "File is too big ({{filesize}}MB). Max filesize: {{maxFilesize}}MB.",
        dictInvalidFileType: "You can't upload files of this type.",
        dictResponseError: "Server responded with {{statusCode}} code.",
        dictCancelUpload: "Cancel upload",
        dictUploadCanceled: "Upload canceled.",
        dictCancelUploadConfirmation: "Are you sure you want to cancel this upload?",
        dictRemoveFile: "Remove file",
        dictMaxFilesExceeded: "You can not upload any more files.",
        
        init: function() {
            const dz = this;
            
            dz.on("sending", function(file, xhr, formData) {
                formData.append("path", currentPath);
            });
            
            dz.on("uploadprogress", async function(file, progress, bytesSent) {
                if (dotNetHelper) {
                    try {
                        await dotNetHelper.invokeMethodAsync('OnUploadProgress', 
                            file.upload.uuid || file.name, // Use UUID as unique identifier
                            file.name, 
                            Math.round(progress), 
                            bytesSent, 
                            file.size);
                    } catch (error) {
                        console.error('Error invoking OnUploadProgress:', error, 'File:', file && file.name);
                    }
                }
            });
            
            dz.on("success", async function(file, response) {
                if (dotNetHelper) {
                    try {
                        await dotNetHelper.invokeMethodAsync('OnFileUploaded', 
                            file.upload.uuid || file.name,
                            file.name);
                    } catch (error) {
                        console.error('Error invoking OnFileUploaded:', error, 'File:', file && file.name);
                    }
                }
            });
            
            dz.on("error", async function(file, errorMessage) {
                if (dotNetHelper) {
                    try {
                        await dotNetHelper.invokeMethodAsync('OnUploadError', 
                            file.upload.uuid || file.name,
                            file.name, 
                            typeof errorMessage === 'string' ? errorMessage : JSON.stringify(errorMessage));
                    } catch (error) {
                        console.error('Error invoking OnUploadError:', error, 'File:', file && file.name);
                    }
                }
            });
            
            dz.on("queuecomplete", async function() {
                if (dotNetHelper) {
                    try {
                        await dotNetHelper.invokeMethodAsync('OnAllUploadsComplete');
                    } catch (error) {
                        console.error('Error invoking OnAllUploadsComplete:', error);
                    }
                }
            });
        }
    });

    return dropzone;
};

window.processDropzoneQueue = function(elementId) {
    try {
        const element = document.getElementById(elementId);
        if (element && element.dropzone) {
            element.dropzone.processQueue();
            return true;
        }
        return false;
    } catch (error) {
        console.error('Error processing dropzone queue:', error);
        return false;
    }
};

window.clearDropzone = function(elementId) {
    try {
        const element = document.getElementById(elementId);
        if (element && element.dropzone) {
            element.dropzone.removeAllFiles(true);
            return true;
        }
        return false;
    } catch (error) {
        console.error('Error clearing dropzone:', error);
        return false;
    }
};

window.destroyDropzone = function(elementId) {
    try {
        const element = document.getElementById(elementId);
        if (element && element.dropzone) {
            element.dropzone.destroy();
            return true;
        }
        return false;
    } catch (error) {
        console.error('Error destroying dropzone:', error);
        return false;
    }
};
