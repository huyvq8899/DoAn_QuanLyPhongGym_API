export function GetLoaiTiens() {
    const list = [
        {
            "Ma": "AED",
            "Ten": "United Arab Emirates Dirham",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "5918.67",
            "SapXep": "118",
            "Status": "0"
        },
        {
            "Ma": "AFN",
            "Ten": "Afghan Afghani",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "372.75",
            "SapXep": "99",
            "Status": "0"
        },
        {
            "Ma": "ALL",
            "Ten": "Albanian Lek",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "175.14",
            "SapXep": "114",
            "Status": "0"
        },
        {
            "Ma": "AMD",
            "Ten": "Armenian Dram",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "45.31",
            "SapXep": "104",
            "Status": "0"
        },
        {
            "Ma": "ANG",
            "Ten": "Netherlands Antillean Guilder",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "12151.9",
            "SapXep": "96",
            "Status": "0"
        },
        {
            "Ma": "AOA",
            "Ten": "Angola",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "199.18",
            "SapXep": "14",
            "Status": "0"
        },
        {
            "Ma": "ARS",
            "Ten": "Argentine Peso",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "2434.49",
            "SapXep": "129",
            "Status": "0"
        },
        {
            "Ma": "AUD",
            "Ten": "Đô la Úc",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "21876",
            "SapXep": "5",
            "Status": "0"
        },
        {
            "Ma": "AWG",
            "Ten": "Aruban Florin",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "12128.31",
            "SapXep": "18",
            "Status": "0"
        },
        {
            "Ma": "AZN",
            "Ten": "Azerbaijani Manat",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "20719.57",
            "SapXep": "148",
            "Status": "0"
        },
        {
            "Ma": "BAM",
            "Ten": "Bosnia-Herzegovina Convertible Mark",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "12576.02",
            "SapXep": "168",
            "Status": "0"
        },
        {
            "Ma": "BBD",
            "Ten": "Barbadian Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "10870",
            "SapXep": "36",
            "Status": "0"
        },
        {
            "Ma": "BDT",
            "Ten": "Bangladeshi Taka",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "281.21",
            "SapXep": "51",
            "Status": "0"
        },
        {
            "Ma": "BGN",
            "Ten": "Bulgarian Lev",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "12581.09",
            "SapXep": "29",
            "Status": "0"
        },
        {
            "Ma": "BHD",
            "Ten": "Bahraini Dinar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "57634.44",
            "SapXep": "160",
            "Status": "0"
        },
        {
            "Ma": "BIF",
            "Ten": "Burundian Franc",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "13.8",
            "SapXep": "39",
            "Status": "0"
        },
        {
            "Ma": "BMD",
            "Ten": "Bermudan Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "21740",
            "SapXep": "124",
            "Status": "0"
        },
        {
            "Ma": "BND",
            "Ten": "Brunei Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "16401.87",
            "SapXep": "59",
            "Status": "0"
        },
        {
            "Ma": "BOB",
            "Ten": "Bolivian Boliviano",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "3164.71",
            "SapXep": "37",
            "Status": "0"
        },
        {
            "Ma": "BRL",
            "Ten": "Brazilian Real",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "7157.39",
            "SapXep": "116",
            "Status": "0"
        },
        {
            "Ma": "BSD",
            "Ten": "Bahamian Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "21740",
            "SapXep": "123",
            "Status": "0"
        },
        {
            "Ma": "BTC",
            "Ten": "Bitcoin",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "5007448.05",
            "SapXep": "46",
            "Status": "0"
        },
        {
            "Ma": "BTN",
            "Ten": "Bhutanese Ngultrum",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "342.21",
            "SapXep": "64",
            "Status": "0"
        },
        {
            "Ma": "BWP",
            "Ten": "Botswanan Pula",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "2228.11",
            "SapXep": "77",
            "Status": "0"
        },
        {
            "Ma": "BYR",
            "Ten": "Belarusian Ruble",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "1.52",
            "SapXep": "109",
            "Status": "0"
        },
        {
            "Ma": "BZD",
            "Ten": "Belize Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "10934.61",
            "SapXep": "108",
            "Status": "0"
        },
        {
            "Ma": "CAD",
            "Ten": "Canadian Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "18145.75",
            "SapXep": "142",
            "Status": "0"
        },
        {
            "Ma": "CDF",
            "Ten": "Congolese Franc",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "23.5",
            "SapXep": "119",
            "Status": "0"
        },
        {
            "Ma": "CHF",
            "Ten": "Swiss Franc",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "23783.65",
            "SapXep": "158",
            "Status": "0"
        },
        {
            "Ma": "CLF",
            "Ten": "Chilean Unit of Account (UF)",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "883668",
            "SapXep": "28",
            "Status": "0"
        },
        {
            "Ma": "CLP",
            "Ten": "Chilean Peso",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "35.54",
            "SapXep": "133",
            "Status": "0"
        },
        {
            "Ma": "CNY",
            "Ten": "Nhân dân tệ Trung Quốc",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "3388",
            "SapXep": "6",
            "Status": "0"
        },
        {
            "Ma": "COP",
            "Ten": "Colombian Peso",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "9.17",
            "SapXep": "135",
            "Status": "0"
        },
        {
            "Ma": "CRC",
            "Ten": "Costa Rican Colón",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "41",
            "SapXep": "164",
            "Status": "0"
        },
        {
            "Ma": "CUC",
            "Ten": "Cuban Convertible Peso",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "21740",
            "SapXep": "48",
            "Status": "0"
        },
        {
            "Ma": "CUP",
            "Ten": "Cuban Peso",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "21792.02",
            "SapXep": "106",
            "Status": "0"
        },
        {
            "Ma": "CVE",
            "Ten": "Cape Verdean Escudo",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "223.05",
            "SapXep": "138",
            "Status": "0"
        },
        {
            "Ma": "CZK",
            "Ten": "Czech Republic Koruna",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "897.99",
            "SapXep": "90",
            "Status": "0"
        },
        {
            "Ma": "DJF",
            "Ten": "Djiboutian Franc",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "122.57",
            "SapXep": "95",
            "Status": "0"
        },
        {
            "Ma": "DKK",
            "Ten": "Danish Krone",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "3347.87",
            "SapXep": "162",
            "Status": "0"
        },
        {
            "Ma": "DOP",
            "Ten": "Dominican Peso",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "487.63",
            "SapXep": "53",
            "Status": "0"
        },
        {
            "Ma": "DZD",
            "Ten": "Algerian Dinar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "224.6",
            "SapXep": "137",
            "Status": "0"
        },
        {
            "Ma": "EEK",
            "Ten": "Estonian Kroon",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "1569.39",
            "SapXep": "103",
            "Status": "0"
        },
        {
            "Ma": "EGP",
            "Ten": "Egyptian Pound",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "2852.79",
            "SapXep": "150",
            "Status": "0"
        },
        {
            "Ma": "ERN",
            "Ten": "Eritrean Nakfa",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "1438.3",
            "SapXep": "121",
            "Status": "0"
        },
        {
            "Ma": "ETB",
            "Ten": "Ethiopian Birr",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "1063.28",
            "SapXep": "35",
            "Status": "0"
        },
        {
            "Ma": "EUR",
            "Ten": "Đồng tiền chung châu Âu",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "27272",
            "SapXep": "3",
            "Status": "0"
        },
        {
            "Ma": "FJD",
            "Ten": "Fijian Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "10742.91",
            "SapXep": "159",
            "Status": "0"
        },
        {
            "Ma": "FKP",
            "Ten": "Falkland Islands Pound",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "33124.13",
            "SapXep": "58",
            "Status": "0"
        },
        {
            "Ma": "GBP",
            "Ten": "Bảng Anh",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "31814",
            "SapXep": "7",
            "Status": "0"
        },
        {
            "Ma": "GEL",
            "Ten": "Georgian Lari",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "9339.98",
            "SapXep": "71",
            "Status": "0"
        },
        {
            "Ma": "GGP",
            "Ten": "Guernsey Pound",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "33124.13",
            "SapXep": "30",
            "Status": "0"
        },
        {
            "Ma": "GHS",
            "Ten": "Ghanaian Cedi",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "5649.25",
            "SapXep": "146",
            "Status": "0"
        },
        {
            "Ma": "GIP",
            "Ten": "Gibraltar Pound",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "33124.13",
            "SapXep": "41",
            "Status": "0"
        },
        {
            "Ma": "GMD",
            "Ten": "Gambian Dalasi",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "505.43",
            "SapXep": "93",
            "Status": "0"
        },
        {
            "Ma": "GNF",
            "Ten": "Guinean Franc",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "3",
            "SapXep": "52",
            "Status": "0"
        },
        {
            "Ma": "GTQ",
            "Ten": "Guatemalan Quetzal",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "2816.5",
            "SapXep": "68",
            "Status": "0"
        },
        {
            "Ma": "GYD",
            "Ten": "Guyanaese Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "105.5",
            "SapXep": "31",
            "Status": "0"
        },
        {
            "Ma": "HKD",
            "Ten": "Đô La Hồng Kong",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "2722",
            "SapXep": "8",
            "Status": "0"
        },
        {
            "Ma": "HNL",
            "Ten": "Honduran Lempira",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "995.3",
            "SapXep": "112",
            "Status": "0"
        },
        {
            "Ma": "HRK",
            "Ten": "Croatian Kuna",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "3246.02",
            "SapXep": "25",
            "Status": "0"
        },
        {
            "Ma": "HTG",
            "Ten": "Haitian Gourde",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "460.26",
            "SapXep": "136",
            "Status": "0"
        },
        {
            "Ma": "HUF",
            "Ten": "Hungarian Forint",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "80.59",
            "SapXep": "62",
            "Status": "0"
        },
        {
            "Ma": "IDR",
            "Ten": "Indonesian Rupiah",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "1.67",
            "SapXep": "117",
            "Status": "0"
        },
        {
            "Ma": "ILS",
            "Ten": "Israeli New Sheqel",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "5643.03",
            "SapXep": "131",
            "Status": "0"
        },
        {
            "Ma": "IMP",
            "Ten": "Manx pound",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "33124.13",
            "SapXep": "40",
            "Status": "0"
        },
        {
            "Ma": "INR",
            "Ten": "Indian Rupee",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "347.73",
            "SapXep": "86",
            "Status": "0"
        },
        {
            "Ma": "IQD",
            "Ten": "Iraqi Dinar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "18.46",
            "SapXep": "72",
            "Status": "0"
        },
        {
            "Ma": "IRR",
            "Ten": "Iranian Rial",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "0.76",
            "SapXep": "75",
            "Status": "0"
        },
        {
            "Ma": "ISK",
            "Ten": "Icelandic Króna",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "166.25",
            "SapXep": "100",
            "Status": "0"
        },
        {
            "Ma": "JEP",
            "Ten": "Jersey Pound",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "33124.13",
            "SapXep": "105",
            "Status": "0"
        },
        {
            "Ma": "JMD",
            "Ten": "Jamaican Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "189.33",
            "SapXep": "167",
            "Status": "0"
        },
        {
            "Ma": "JOD",
            "Ten": "Jordanian Dinar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "30685.75",
            "SapXep": "91",
            "Status": "0"
        },
        {
            "Ma": "JPY",
            "Ten": "Yên Nhật",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "221.54",
            "SapXep": "4",
            "Status": "0"
        },
        {
            "Ma": "KES",
            "Ten": "Kenyan Shilling",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "228.57",
            "SapXep": "120",
            "Status": "0"
        },
        {
            "Ma": "KGS",
            "Ten": "Kyrgystani Som",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "365.25",
            "SapXep": "139",
            "Status": "0"
        },
        {
            "Ma": "KHR",
            "Ten": "Riêl Cămpuchia",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "5.23",
            "SapXep": "9",
            "Status": "0"
        },
        {
            "Ma": "KMF",
            "Ten": "Comorian Franc",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "50.02",
            "SapXep": "78",
            "Status": "0"
        },
        {
            "Ma": "KPW",
            "Ten": "North Korean Won",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "24.15",
            "SapXep": "85",
            "Status": "0"
        },
        {
            "Ma": "KRW",
            "Ten": "South Korean Won",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "20.05",
            "SapXep": "27",
            "Status": "0"
        },
        {
            "Ma": "KWD",
            "Ten": "Kuwaiti Dinar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "73282.69",
            "SapXep": "145",
            "Status": "0"
        },
        {
            "Ma": "KYD",
            "Ten": "Cayman Islands Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "26449.62",
            "SapXep": "161",
            "Status": "0"
        },
        {
            "Ma": "KZT",
            "Ten": "Kazakhstani Tenge",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "117.27",
            "SapXep": "43",
            "Status": "0"
        },
        {
            "Ma": "LAK",
            "Ten": "Kíp Lào",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "2.65",
            "SapXep": "10",
            "Status": "0"
        },
        {
            "Ma": "LBP",
            "Ten": "Lebanese Pound",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "14.44",
            "SapXep": "16",
            "Status": "0"
        },
        {
            "Ma": "LKR",
            "Ten": "Sri Lankan Rupee",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "163.94",
            "SapXep": "66",
            "Status": "0"
        },
        {
            "Ma": "LRD",
            "Ten": "Liberian Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "257.12",
            "SapXep": "54",
            "Status": "0"
        },
        {
            "Ma": "LSL",
            "Ten": "Lesotho Loti",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "1811.81",
            "SapXep": "26",
            "Status": "0"
        },
        {
            "Ma": "LTL",
            "Ten": "Lithuanian Litas",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "7287.79",
            "SapXep": "84",
            "Status": "0"
        },
        {
            "Ma": "LVL",
            "Ten": "Latvian Lats",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "34980.23",
            "SapXep": "42",
            "Status": "0"
        },
        {
            "Ma": "LYD",
            "Ten": "Libyan Dinar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "16082.71",
            "SapXep": "130",
            "Status": "0"
        },
        {
            "Ma": "MAD",
            "Ten": "Moroccan Dirham",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "2254.55",
            "SapXep": "134",
            "Status": "0"
        },
        {
            "Ma": "MDL",
            "Ten": "Moldovan Leu",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "1211.03",
            "SapXep": "80",
            "Status": "0"
        },
        {
            "Ma": "MGA",
            "Ten": "Malagasy Ariary",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "7.04",
            "SapXep": "47",
            "Status": "0"
        },
        {
            "Ma": "MKD",
            "Ten": "Macedonian Denar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "399.6",
            "SapXep": "17",
            "Status": "0"
        },
        {
            "Ma": "MMK",
            "Ten": "Myanma Kyat",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "20.09",
            "SapXep": "73",
            "Status": "0"
        },
        {
            "Ma": "MNT",
            "Ten": "Mongolian Tugrik",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "11.13",
            "SapXep": "153",
            "Status": "0"
        },
        {
            "Ma": "MOP",
            "Ten": "Macanese Pataca",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "2729.15",
            "SapXep": "152",
            "Status": "0"
        },
        {
            "Ma": "MRO",
            "Ten": "Mauritanian Ouguiya",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "70.57",
            "SapXep": "107",
            "Status": "0"
        },
        {
            "Ma": "MTL",
            "Ten": "Maltese Lira",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "57393.58",
            "SapXep": "82",
            "Status": "0"
        },
        {
            "Ma": "MUR",
            "Ten": "Mauritian Rupee",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "620.94",
            "SapXep": "56",
            "Status": "0"
        },
        {
            "Ma": "MVR",
            "Ten": "Maldivian Rufiyaa",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "1423.17",
            "SapXep": "22",
            "Status": "0"
        },
        {
            "Ma": "MWK",
            "Ten": "Malawian Kwacha",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "50.37",
            "SapXep": "55",
            "Status": "0"
        },
        {
            "Ma": "MXN",
            "Ten": "Mexican Peso",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "1415.45",
            "SapXep": "98",
            "Status": "0"
        },
        {
            "Ma": "MYR",
            "Ten": "Malaysian Ringgit",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "6090.05",
            "SapXep": "144",
            "Status": "0"
        },
        {
            "Ma": "MZN",
            "Ten": "Mozambican Metical",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "615.87",
            "SapXep": "63",
            "Status": "0"
        },
        {
            "Ma": "NAD",
            "Ten": "Namibian Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "1811.81",
            "SapXep": "169",
            "Status": "0"
        },
        {
            "Ma": "NGN",
            "Ten": "Nigerian Naira",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "109.48",
            "SapXep": "154",
            "Status": "0"
        },
        {
            "Ma": "NIO",
            "Ten": "Nicaraguan Córdoba",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "814.32",
            "SapXep": "143",
            "Status": "0"
        },
        {
            "Ma": "NOK",
            "Ten": "Norwegian Krone",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "2967.72",
            "SapXep": "32",
            "Status": "0"
        },
        {
            "Ma": "NPR",
            "Ten": "Nepalese Rupee",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "215.05",
            "SapXep": "151",
            "Status": "0"
        },
        {
            "Ma": "NZD",
            "Ten": "New Zealand Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "16308.61",
            "SapXep": "20",
            "Status": "0"
        },
        {
            "Ma": "OMR",
            "Ten": "Omani Rial",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "56479.85",
            "SapXep": "127",
            "Status": "0"
        },
        {
            "Ma": "PAB",
            "Ten": "Panamanian Balboa",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "21740",
            "SapXep": "70",
            "Status": "0"
        },
        {
            "Ma": "PEN",
            "Ten": "Peruvian Nuevo Sol",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "6921.15",
            "SapXep": "57",
            "Status": "0"
        },
        {
            "Ma": "PGK",
            "Ten": "Papua New Guinean Kina",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "8078.74",
            "SapXep": "157",
            "Status": "0"
        },
        {
            "Ma": "PHP",
            "Ten": "Philippine Peso",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "487.08",
            "SapXep": "87",
            "Status": "0"
        },
        {
            "Ma": "PKR",
            "Ten": "Pakistani Rupee",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "214.2",
            "SapXep": "97",
            "Status": "0"
        },
        {
            "Ma": "PLN",
            "Ten": "Polish Zloty",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "6083.81",
            "SapXep": "88",
            "Status": "0"
        },
        {
            "Ma": "PYG",
            "Ten": "Paraguayan Guarani",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "4.35",
            "SapXep": "61",
            "Status": "0"
        },
        {
            "Ma": "QAR",
            "Ten": "Qatari Rial",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "5971.83",
            "SapXep": "69",
            "Status": "0"
        },
        {
            "Ma": "RON",
            "Ten": "Romanian Leu",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "5540.16",
            "SapXep": "24",
            "Status": "0"
        },
        {
            "Ma": "RSD",
            "Ten": "Serbian Dinar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "204.05",
            "SapXep": "132",
            "Status": "0"
        },
        {
            "Ma": "RUB",
            "Ten": "Ruble Nga",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "435.97",
            "SapXep": "15",
            "Status": "0"
        },
        {
            "Ma": "RWF",
            "Ten": "Rwandan Franc",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "31.63",
            "SapXep": "155",
            "Status": "0"
        },
        {
            "Ma": "SAR",
            "Ten": "Saudi Riyal",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "5971.29",
            "SapXep": "38",
            "Status": "0"
        },
        {
            "Ma": "SBD",
            "Ten": "Solomon Islands Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "2820.42",
            "SapXep": "128",
            "Status": "0"
        },
        {
            "Ma": "SCR",
            "Ten": "Seychellois Rupee",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "1602.24",
            "SapXep": "92",
            "Status": "0"
        },
        {
            "Ma": "SDG",
            "Ten": "Sudanese Pound",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "3662.02",
            "SapXep": "76",
            "Status": "0"
        },
        {
            "Ma": "SEK",
            "Ten": "Swedish Krona",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "2670.95",
            "SapXep": "65",
            "Status": "0"
        },
        {
            "Ma": "SGD",
            "Ten": "Đô la Singapore",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "16911",
            "SapXep": "11",
            "Status": "0"
        },
        {
            "Ma": "SHP",
            "Ten": "Saint Helena Pound",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "33124.13",
            "SapXep": "89",
            "Status": "0"
        },
        {
            "Ma": "SLL",
            "Ten": "Sierra Leonean Leone",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "5.02",
            "SapXep": "81",
            "Status": "0"
        },
        {
            "Ma": "SOS",
            "Ten": "Somali Shilling",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "31.11",
            "SapXep": "141",
            "Status": "0"
        },
        {
            "Ma": "SRD",
            "Ten": "Surinamese Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "6554.11",
            "SapXep": "34",
            "Status": "0"
        },
        {
            "Ma": "STD",
            "Ten": "São Tomé and Príncipe Dobra",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "1",
            "SapXep": "49",
            "Status": "0"
        },
        {
            "Ma": "SVC",
            "Ten": "Salvadoran Colón",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "2502.43",
            "SapXep": "60",
            "Status": "0"
        },
        {
            "Ma": "SYP",
            "Ten": "Syrian Pound",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "115.07",
            "SapXep": "125",
            "Status": "0"
        },
        {
            "Ma": "SZL",
            "Ten": "Swazi Lilangeni",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "1811.7",
            "SapXep": "110",
            "Status": "0"
        },
        {
            "Ma": "THB",
            "Ten": "Bath Thái",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "735",
            "SapXep": "12",
            "Status": "0"
        },
        {
            "Ma": "TJS",
            "Ten": "Tajikistani Somoni",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "3459.81",
            "SapXep": "147",
            "Status": "0"
        },
        {
            "Ma": "TMT",
            "Ten": "Turkmenistani Manat",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "6211.38",
            "SapXep": "102",
            "Status": "0"
        },
        {
            "Ma": "TND",
            "Ten": "Tunisian Dinar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "11421.37",
            "SapXep": "33",
            "Status": "0"
        },
        {
            "Ma": "TOP",
            "Ten": "Tongan Paʻanga",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "10731.17",
            "SapXep": "45",
            "Status": "0"
        },
        {
            "Ma": "TRY",
            "Ten": "Turkish Lira",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "8054.76",
            "SapXep": "94",
            "Status": "0"
        },
        {
            "Ma": "TTD",
            "Ten": "Trinidad and Tobago Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "3439.12",
            "SapXep": "44",
            "Status": "0"
        },
        {
            "Ma": "TWD",
            "Ten": "New Taiwan Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "709.25",
            "SapXep": "140",
            "Status": "0"
        },
        {
            "Ma": "TZS",
            "Ten": "Tanzanian Shilling",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "11.02",
            "SapXep": "23",
            "Status": "0"
        },
        {
            "Ma": "UAH",
            "Ten": "Grip-na Ucraina",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "2600",
            "SapXep": "13",
            "Status": "0"
        },
        {
            "Ma": "UGX",
            "Ten": "Ugandan Shilling",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "7.26",
            "SapXep": "19",
            "Status": "0"
        },
        {
            "Ma": "USD",
            "Ten": "Đô la Mỹ",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "20960",
            "SapXep": "2",
            "Status": "1"
        },
        {
            "Ma": "UYU",
            "Ten": "Uruguayan Peso",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "826.19",
            "SapXep": "126",
            "Status": "0"
        },
        {
            "Ma": "UZS",
            "Ten": "Uzbekistan Som",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "8.65",
            "SapXep": "101",
            "Status": "0"
        },
        {
            "Ma": "VEF",
            "Ten": "Venezuelan Bolívar Fuerte",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "3440.7",
            "SapXep": "163",
            "Status": "0"
        },
        {
            "Ma": "VND",
            "Ten": "Việt Nam đồng",
            "TaiKhoanTienMat": "1111",
            "TaiKhoanNganHang": "1121",
            "TyGiaQuyDoi": "1",
            "SapXep": "1",
            "Status": "1"
        },
        {
            "Ma": "VUV",
            "Ten": "Vanuatu Vatu",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "206.75",
            "SapXep": "165",
            "Status": "0"
        },
        {
            "Ma": "WST",
            "Ten": "Samoan Tala",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "8900.62",
            "SapXep": "149",
            "Status": "0"
        },
        {
            "Ma": "XAF",
            "Ten": "CFA Franc BEAC",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "37.44",
            "SapXep": "111",
            "Status": "0"
        },
        {
            "Ma": "XAG",
            "Ten": "Silver (troy ounce)",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "358008.17",
            "SapXep": "156",
            "Status": "0"
        },
        {
            "Ma": "XAU",
            "Ten": "Gold (troy ounce)",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "25841574.75",
            "SapXep": "122",
            "Status": "0"
        },
        {
            "Ma": "XCD",
            "Ten": "East Caribbean Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "8048.82",
            "SapXep": "166",
            "Status": "0"
        },
        {
            "Ma": "XDR",
            "Ten": "Special Drawing Rights",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "30596.53",
            "SapXep": "50",
            "Status": "0"
        },
        {
            "Ma": "XOF",
            "Ten": "CFA Franc BCEAO",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "37.48",
            "SapXep": "21",
            "Status": "0"
        },
        {
            "Ma": "XPF",
            "Ten": "CFP Franc",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "206.1",
            "SapXep": "67",
            "Status": "0"
        },
        {
            "Ma": "YER",
            "Ten": "Yemeni Rial",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "101.13",
            "SapXep": "74",
            "Status": "0"
        },
        {
            "Ma": "ZAR",
            "Ten": "South African Rand",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "1808.57",
            "SapXep": "83",
            "Status": "0"
        },
        {
            "Ma": "ZMK",
            "Ten": "Zambian Kwacha (pre-2013)",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "4.13",
            "SapXep": "115",
            "Status": "0"
        },
        {
            "Ma": "ZMW",
            "Ten": "Zambian Kwacha",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "2968.12",
            "SapXep": "113",
            "Status": "0"
        },
        {
            "Ma": "ZWL",
            "Ten": "Zimbabwean Dollar",
            "TaiKhoanTienMat": "1112",
            "TaiKhoanNganHang": "1122",
            "TyGiaQuyDoi": "67.44",
            "SapXep": "79",
            "Status": "0"
        }
    ];

    list.sort((a: any, b: any) => parseInt(a.SapXep, 0) - parseInt(b.SapXep, 0));
    return list;
};
