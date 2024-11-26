document.addEventListener("DOMContentLoaded", () => {
    // Lấy phần tử và các biến cần thiết
    const changeBtn = document.getElementById("change-btn");  // Nút để thay đổi trạng thái hiển thị
    const searchSection = document.querySelector(".search-section");  // Phần tử search-section
    const toggleFilterBtn = document.getElementById("toggle-filter-btn");  // Nút để hiển thị/ẩn bộ lọc
    const filterContent = document.querySelector(".filter-content");  // Bộ lọc
    const priceRange = document.getElementById("priceRange");  // Thanh kéo giá
    const priceValue = document.getElementById("priceValue");  // Hiển thị giá trị thanh kéo

    // Mảng chuyến bay (ví dụ)
    const flights = [
        {
            departureTime: "08:00",
            arrivalTime: "10:00",
            departure: "Hà Nội",
            arrival: "Hồ Chí Minh",
            duration: "2h",
            flightCode: "VN123",
            stops: "Bay thẳng",
        },
        {
            departureTime: "12:00",
            arrivalTime: "14:30",
            departure: "Hà Nội",
            arrival: "Đà Nẵng",
            duration: "2h30m",
            flightCode: "VN456",
            stops: "Bay thẳng",
        },
    ];

    // Hàm render chuyến bay
    function renderFlights() {
        flightList.innerHTML = "";
        flights.forEach((flight) => {
            const flightItem = document.createElement("div");
            flightItem.classList.add("flight-item");
            flightItem.innerHTML = `
        <div class="details">
          <strong>${flight.departureTime} - ${flight.arrivalTime}</strong>
          <span>${flight.departure} → ${flight.arrival}</span>
          <span>${flight.stops}</span>
        </div>
        <div>
          <span>${flight.duration}</span>
          <span>${flight.flightCode}</span>
          <button class="details-btn">Chi tiết chuyến bay</button>
        </div>
        <button class="select-btn">Chọn</button>
      `;
            flightList.appendChild(flightItem);
        });
    }

    // Hiển thị hoặc ẩn phần search-section khi bấm nút #change-btn
    changeBtn.addEventListener("click", () => {
        searchSection.classList.toggle("hidden");  // Thêm/loại bỏ lớp 'hidden'
    });

    // Hiển thị/ẩn bộ lọc
    toggleFilterBtn.addEventListener("click", () => {
        filterContent.classList.toggle("active");
    });

    // Cập nhật giá trị thanh kéo (giá) khi thay đổi
    priceRange.addEventListener("input", () => {
        priceValue.textContent = priceRange.value.toLocaleString(); // Cập nhật giá trị
    });

    // Render danh sách chuyến bay khi trang tải xong
    renderFlights();
});


// Bảng chọn số lượng khách hàng

const passengersInput = document.getElementById("passengers");
const passengerPanel = document.getElementById("passenger-panel");
const confirmPassengersBtn = document.getElementById("confirm-passengers");
const searchFlightBtn = document.getElementById("search-btn");

// Hiển thị bảng chọn số lượng hành khách khi click vào input
passengersInput.addEventListener("click", function () {
    passengerPanel.classList.toggle("hidden");
});

// Xác nhận số lượng hành khách và đóng bảng khi click "Xong"
confirmPassengersBtn.addEventListener("click", function (event) {
    event.stopPropagation();
    passengerPanel.classList.add("hidden");
    updatePassengersCount();
    event.preventDefault(); // Ngăn chặn chuyển hướng trang mặc định
});

// Cập nhật thông tin số lượng hành khách
function updatePassengersCount() {
    const adultCount = parseInt(document.querySelector(".passenger-category:nth-child(1) .count").textContent);
    const childCount = parseInt(document.querySelector(".passenger-category:nth-child(2) .count").textContent);
    const infantCount = parseInt(document.querySelector(".passenger-category:nth-child(3) .count").textContent);

    // Cập nhật giá trị cho trường input
    passengersInput.value = `${adultCount} người lớn, ${childCount} trẻ em, ${infantCount} em bé`;
}

// Tăng số lượng hành khách
const increaseButtons = document.querySelectorAll(".increase");
increaseButtons.forEach(button => {
    button.addEventListener("click", function (event) {
        event.preventDefault();
        const countSpan = button.previousElementSibling;
        let count = parseInt(countSpan.textContent);
        count++;
        countSpan.textContent = count;
        updatePassengersCount();
    });
});

// Giảm số lượng hành khách
const decreaseButtons = document.querySelectorAll(".decrease");
decreaseButtons.forEach(button => {
    button.addEventListener("click", function (event) {
        event.preventDefault();
        const countSpan = button.nextElementSibling;
        let count = parseInt(countSpan.textContent);
        if (count > 0) {
            count--;
            countSpan.textContent = count;
            updatePassengersCount();
        }
    });
});

// Cơ chế tìm kiếm (chưa implement logic tìm kiếm thực tế)
searchFlightBtn.addEventListener("click", function (event) {
    event.preventDefault();
    // Thêm logic tìm kiếm chuyến bay ở đây
});

//Vô hiệu hóa khi click radio một chiều
document.addEventListener('DOMContentLoaded', function () {
    const oneWayRadio = document.getElementById('one-way');
    const roundTripRadio = document.getElementById('round-trip');
    const returnDateInput = document.getElementById('return-date');

    // Hàm cập nhật trạng thái của ngày về
    function updateReturnDateField() {
        if (oneWayRadio.checked) {
            returnDateInput.disabled = true; // Vô hiệu hóa khi chọn "Một chiều"
            returnDateInput.value = ''; // Xóa giá trị trong trường Ngày về
        } else if (roundTripRadio.checked) {
            returnDateInput.disabled = false; // Kích hoạt khi chọn "Khứ hồi"
        }
    }

    // Lắng nghe sự kiện khi người dùng thay đổi lựa chọn radio
    oneWayRadio.addEventListener('change', updateReturnDateField);
    roundTripRadio.addEventListener('change', updateReturnDateField);

    // Khởi tạo trạng thái ban đầu của trường Ngày về khi trang được tải
    updateReturnDateField();
});



$(document).ready(function() {
    // Bật/tắt hiển thị bộ lọc
    $('#toggle-filter-btn').on('click', function() {
        $('.filter-content').toggleClass('active');  // Hiển thị hoặc ẩn phần lọc
    });

    // Mở rộng/thu gọn nội dung của từng bộ lọc
    $('.filter-title').on('click', function() {
        var target = $(this).data('target');
        $(target).toggleClass('active');  // Hiển thị hoặc ẩn phần chi tiết của bộ lọc
        $(this).find('.arrow').toggleClass('up down');  // Thay đổi biểu tượng mũi tên
    });

    // Hiển thị giá trị của thanh kéo khi thay đổi
    $('#priceRange').on('input', function() {
        var value = $(this).val();
        $('#priceValue').text(value.toLocaleString());  // Cập nhật giá trị hiển thị
    });
});

//Danh sách các chuyến bay



