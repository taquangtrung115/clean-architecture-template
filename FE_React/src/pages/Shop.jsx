import React, { useEffect, useState, useMemo } from "react";
import {
  Filters,
  Pagination,
  ProductElement,
  SectionTitle,
} from "../components";
import "../styles/Shop.css";
import { toast } from "react-toastify";
import { nanoid } from "nanoid";
import CallAPI from "../utils/callApi";

export const ShopLoad = () => {
  const [data, setData] = useState([]);
  const [dataFilter, setDataFilter] = useState({
    Categories: [],
    Brands: [],
  });
  useMemo(() => {
    const getDataFilters = async () => {
      try {
        const [linhVucRes, customerRes, f2024Res] = await Promise.all([
          GetLinhVucApp(),
          GetKhachHang(),
          GetF2024(),
        ]);
        setDataFilter(prev => ({
          ...prev,
          LinhVuc: linhVucRes.data?.isSuccess
            ? linhVucRes.data?.listData.map((x) => ({
              label: x.tenLinhVuc,
              value: x.maLinhVuc
            }))
            : [],
          KhachHang: customerRes.data?.isSuccess
            ? customerRes.data?.listData.map((x) => ({
              label: x.tenKH,
              value: x.maKH
            }))
            : [],
          KySu: f2024Res.data?.isSuccess
            ? f2024Res.data?.listData.map((x) => ({
              label: x.tenKySu,
              value: x.maKysu
            }))
            : [],
        }));
        message.success("Lấy dữ liệu bộ lọc thành công");
      } catch (error) {
        message.error("Đã xảy ra lỗi");
      } finally {
        setLoading(false);
      }
    };
    getDataFilters();
  }, []);
  useEffect(() => {
    const fetchProducts = async () => {
      let parameter = `?PageNumber=1&PageSize=20`;
      try {
        const res = await CallAPI(`v1/products/getAll${parameter}`, "GET");
        if (res && res.data && res.data.items) {
          setData(res.data.items); // Correctly set the response data
        } else {
          toast.warn("Error fetching products");
        }
      } catch (err) {
        toast.error("Failed due to: " + err.message);
      }
    };

    fetchProducts();
  }, []); // Empty dependency array to run only once on component mount

  return (
    <>
      <SectionTitle title="Shop" path="Home | Shop" />
      <div className="max-w-7xl mx-auto mt-5">
        <Filters />
        {data.length === 0 && (
          <h2 className="text-accent-content text-center text-4xl my-10">
            No products found for this filter
          </h2>
        )}
        <div className="grid grid-cols-4 px-2 gap-y-4 max-lg:grid-cols-3 max-md:grid-cols-2 max-sm:grid-cols-1 shop-products-grid">
          {data.length !== 0 &&
            data.map((product) => (
              <ProductElement
                key={nanoid()}
                id={product.id}
                title={product.productName}
                image={product.imageUrl}
                rating={product.rating}
                price={product.price}
                brandName={product.brandName}
              />
            ))}
        </div>
      </div>

      <Pagination data={data} />
    </>
  );
};

export default ShopLoad;