/*!
    * Start Bootstrap - SB Admin v7.0.7 (https://startbootstrap.com/template/sb-admin)
    * Copyright 2013-2023 Start Bootstrap
    * Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-sb-admin/blob/master/LICENSE)
    */
    // 
// Scripts
// 

window.addEventListener('DOMContentLoaded', event => {

    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        // Uncomment Below to persist sidebar toggle between refreshes
        // if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        //     document.body.classList.toggle('sb-sidenav-toggled');
        // }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

});

//
document.querySelectorAll('.nav-item').forEach(item => {
    item.addEventListener('click', () => {
        // Xóa lớp active khỏi tất cả các phần tử
        document.querySelectorAll('.nav-item').forEach(el => el.classList.remove('active'));
        // Thêm lớp active vào phần tử được click
        item.classList.add('active');
    });
});


document.getElementsByClassName("myDate").min = new Date().getFullYear() + "-" + parseInt(new Date().getMonth() + 1) + "-" + new Date().getDate()

document.addEventListener('DOMContentLoaded', () => {
    const openBoxBtn = document.getElementById('openBoxBtn');
    const box = document.getElementById('box');
    const flightClassOptions = document.getElementById('flightClassOptions');
    const selectedFlightClass = document.getElementById('selectedFlightClass');
    const submitBtn = document.getElementById('submitBtn');

    // Hiển thị hoặc ẩn box khi nhấn vào button
    openBoxBtn.addEventListener('click', () => {
        box.style.display = box.style.display === 'none' ? 'block' : 'none';
    });

    // Xử lý khi chọn hạng máy bay
    selectedFlightClass.addEventListener('click', () => {
        flightClassOptions.classList.toggle('open');
    });

    // Lựa chọn hạng máy bay
    flightClassOptions.addEventListener('click', (event) => {
        const value = event.target.getAttribute('data-value');
        if (value) {
            selectedFlightClass.textContent = event.target.textContent;
            flightClassOptions.classList.remove('open');
        }
    });

    // Xử lý khi nhấn nút xác nhận
    submitBtn.addEventListener('click', () => {
        const flightClass = selectedFlightClass.textContent;
        const adultCount = document.getElementById('adultCount').value;
        const childCount = document.getElementById('childCount').value;

        // Hiển thị kết quả
        alert(`Hạng máy bay: ${flightClass}\nSố lượng người lớn: ${adultCount}\nSố lượng trẻ em: ${childCount}`);
    });
});

