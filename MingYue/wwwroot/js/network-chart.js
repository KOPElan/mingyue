// Network Traffic Chart
let networkChart = null;
let chartCanvas = null;
let chartContext = null;

window.initNetworkChart = function() {
    chartCanvas = document.getElementById('networkTrafficChart');
    if (!chartCanvas) {
        console.error('Chart canvas not found');
        return;
    }
    
    chartContext = chartCanvas.getContext('2d');
    
    // Set canvas size
    const container = chartCanvas.parentElement;
    chartCanvas.width = container.clientWidth;
    chartCanvas.height = 300;
    
    // Initialize with empty chart
    drawChart([], [], []);
};

window.updateNetworkChart = function(labels, receivedData, sentData) {
    if (!chartContext || !chartCanvas) {
        console.error('Chart not initialized');
        return;
    }
    
    drawChart(labels, receivedData, sentData);
};

function drawChart(labels, receivedData, sentData) {
    const ctx = chartContext;
    const canvas = chartCanvas;
    const width = canvas.width;
    const height = canvas.height;
    
    // Clear canvas
    ctx.clearRect(0, 0, width, height);
    
    // Set colors based on theme
    const isDark = document.documentElement.getAttribute('data-theme') === 'dark';
    const bgColor = isDark ? '#1e1e1e' : '#ffffff';
    const gridColor = isDark ? '#333333' : '#e0e0e0';
    const textColor = isDark ? '#cccccc' : '#333333';
    const receivedColor = '#0078d4'; // Blue for received
    const sentColor = '#00cc6a'; // Green for sent
    
    // Fill background
    ctx.fillStyle = bgColor;
    ctx.fillRect(0, 0, width, height);
    
    // Chart margins
    const margin = { top: 20, right: 20, bottom: 40, left: 60 };
    const chartWidth = width - margin.left - margin.right;
    const chartHeight = height - margin.top - margin.bottom;
    
    if (receivedData.length === 0) {
        // Draw empty state
        ctx.fillStyle = textColor;
        ctx.font = '14px sans-serif';
        ctx.textAlign = 'center';
        ctx.fillText('等待数据...', width / 2, height / 2);
        return;
    }
    
    // Find max value for scaling
    const maxReceived = Math.max(...receivedData, 1);
    const maxSent = Math.max(...sentData, 1);
    const maxValue = Math.max(maxReceived, maxSent, 1024); // Minimum 1KB scale
    
    // Draw grid lines
    ctx.strokeStyle = gridColor;
    ctx.lineWidth = 1;
    const numGridLines = 5;
    for (let i = 0; i <= numGridLines; i++) {
        const y = margin.top + (chartHeight * i / numGridLines);
        ctx.beginPath();
        ctx.moveTo(margin.left, y);
        ctx.lineTo(width - margin.right, y);
        ctx.stroke();
        
        // Draw Y-axis labels
        const value = maxValue * (1 - i / numGridLines);
        ctx.fillStyle = textColor;
        ctx.font = '11px sans-serif';
        ctx.textAlign = 'right';
        ctx.fillText(formatBytesShort(value), margin.left - 5, y + 4);
    }
    
    // Draw X-axis
    ctx.strokeStyle = gridColor;
    ctx.beginPath();
    ctx.moveTo(margin.left, height - margin.bottom);
    ctx.lineTo(width - margin.right, height - margin.bottom);
    ctx.stroke();
    
    // Draw Y-axis
    ctx.beginPath();
    ctx.moveTo(margin.left, margin.top);
    ctx.lineTo(margin.left, height - margin.bottom);
    ctx.stroke();
    
    // Draw data lines
    function drawLine(data, color, label) {
        if (data.length < 2) return;
        
        ctx.strokeStyle = color;
        ctx.lineWidth = 2;
        ctx.beginPath();
        
        for (let i = 0; i < data.length; i++) {
            const x = margin.left + (chartWidth * i / (data.length - 1));
            const y = height - margin.bottom - (chartHeight * data[i] / maxValue);
            
            if (i === 0) {
                ctx.moveTo(x, y);
            } else {
                ctx.lineTo(x, y);
            }
        }
        
        ctx.stroke();
        
        // Fill area under line with transparency
        ctx.lineTo(margin.left + chartWidth, height - margin.bottom);
        ctx.lineTo(margin.left, height - margin.bottom);
        ctx.closePath();
        ctx.fillStyle = color + '20'; // Add transparency
        ctx.fill();
    }
    
    // Draw received data (blue)
    drawLine(receivedData, receivedColor, '接收');
    
    // Draw sent data (green)
    drawLine(sentData, sentColor, '发送');
    
    // Draw X-axis labels (show only a few to avoid crowding)
    ctx.fillStyle = textColor;
    ctx.font = '11px sans-serif';
    ctx.textAlign = 'center';
    const labelStep = Math.max(1, Math.floor(labels.length / 6));
    for (let i = 0; i < labels.length; i += labelStep) {
        const x = margin.left + (chartWidth * i / (labels.length - 1));
        ctx.fillText(labels[i], x, height - margin.bottom + 15);
    }
    
    // Draw legend
    const legendY = margin.top - 5;
    const legendX = width - margin.right - 200;
    
    // Received legend
    ctx.fillStyle = receivedColor;
    ctx.fillRect(legendX, legendY, 15, 10);
    ctx.fillStyle = textColor;
    ctx.font = '12px sans-serif';
    ctx.textAlign = 'left';
    ctx.fillText('接收速率', legendX + 20, legendY + 9);
    
    // Sent legend
    ctx.fillStyle = sentColor;
    ctx.fillRect(legendX + 90, legendY, 15, 10);
    ctx.fillStyle = textColor;
    ctx.fillText('发送速率', legendX + 110, legendY + 9);
    
    // Draw axis labels
    ctx.fillStyle = textColor;
    ctx.font = '12px sans-serif';
    ctx.textAlign = 'center';
    ctx.fillText('时间', width / 2, height - 5);
    
    ctx.save();
    ctx.translate(15, height / 2);
    ctx.rotate(-Math.PI / 2);
    ctx.fillText('流量速率 (字节/秒)', 0, 0);
    ctx.restore();
}

function formatBytesShort(bytes) {
    if (bytes < 1024) return bytes.toFixed(0) + 'B/s';
    if (bytes < 1024 * 1024) return (bytes / 1024).toFixed(1) + 'KB/s';
    if (bytes < 1024 * 1024 * 1024) return (bytes / (1024 * 1024)).toFixed(1) + 'MB/s';
    return (bytes / (1024 * 1024 * 1024)).toFixed(1) + 'GB/s';
}

// Handle window resize
window.addEventListener('resize', function() {
    if (chartCanvas && chartContext) {
        const container = chartCanvas.parentElement;
        if (container) {
            chartCanvas.width = container.clientWidth;
            chartCanvas.height = 300;
        }
    }
});
