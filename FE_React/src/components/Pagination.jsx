import React, { useState } from "react";
import { FaCircleArrowLeft } from "react-icons/fa6";
import { FaCircleArrowRight } from "react-icons/fa6";
import { useLoaderData, useLocation, useNavigate } from "react-router-dom";

const Pagination = (data) => {

  const { search, pathname } = useLocation();
  const navigate = useNavigate();
  const handlePageChange = (pageNumber) => {
    // const searchParams = new URLSearchParams(search);
    // searchParams.set('page', pageNumber);
    // navigate(`${pathname}?${searchParams.toString()}`);
  };

  return (
    <>
      <div className="pagination flex justify-center mt-10">
        <div className="join">
          <button
            className="join-item btn text-4xl flex justify-center"
            onClick={() => {

              if (data.page === 1) {
                return;
              }
              handlePageChange(data.itemsFrom)
              window.scrollTo(0, 0)

            }}
          >
            <FaCircleArrowLeft />
          </button>
          <button className="join-item btn text-2xl">Page {data.itemsFrom}</button>
          <button
            className="join-item btn text-4xl flex justify-center"
            onClick={() => {

              if (data.items < 10) {
                return;
              }

              handlePageChange(data.itemsFrom)
              window.scrollTo(0, 0)
            }
            }
          >
            <FaCircleArrowRight />
          </button>
        </div>
      </div>
    </>

  );
};

export default Pagination;
