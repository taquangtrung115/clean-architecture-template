import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { SectionTitle } from "../components";
import { nanoid } from "nanoid";
import { toast } from "react-toastify";
import CallAPI from "../utils/callApi";

const Register = () => {
  const [formData, setFormData] = useState({
    lastname: "",
    email: "",
    password: "",
    confirmPassword: "",
    phone: "",
    address: "",
    username: "",
    name: "",

  });

  const navigate = useNavigate();

  const isValidate = () => {
    let isProceed = true;
    let errorMessage = "";
    formData.name = formData.username + " " + formData.lastname;
    if (formData.username.length === 0) {
      isProceed = false;
      errorMessage = "Please enter the value in username field";
    } else if (formData.lastname.length === 0) {
      isProceed = false;
      errorMessage = "Please enter the value in lastname field";
    } else if (formData.email.length === 0) {
      isProceed = false;
      errorMessage = "Please enter the value in email field";
    } else if (formData.phone.length < 4) {
      isProceed = false;
      errorMessage = "Phone must be longer than 3 characters";
    } else if (formData.address.length < 4) {
      isProceed = false;
      errorMessage = "Adress must be longer than 3 characters";
    } else if (formData.password.length < 6) {
      isProceed = false;
      errorMessage = "Please enter a password longer than 5 characters";
    } else if (formData.confirmPassword.length < 6) {
      isProceed = false;
      errorMessage = "Please enter a confirm password longer than 5 characters";
    } else if (formData.password !== formData.confirmPassword) {
      isProceed = false;
      errorMessage = "Passwords must match";
    }

    if (!isProceed) {
      toast.warn(errorMessage);
    }

    return isProceed;
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    let regObj = {
      id: nanoid(),
      ...formData,
      userWishlist: [],
    };

    if (isValidate()) {
      CallAPI("identity/register", "POST", regObj)
        .then((res) => {
          debugger;
          if (res && res.data?.succeeded === true) {
            toast.success("Registration Successful");
            navigate("/login");
          }
          else if (res && res.data?.errors) {
            toast.warn("Failed: " + res.data?.errors[0].description);
          }
          else {
            toast.error("Failed network");
          }
        })
        .catch((err) => {
          debugger;
          toast.error("Failed: " + err.message);
        });
    }
  };

  return (
    <>
      <SectionTitle title="Register" path="Home | Register" />
      <div className="flex flex-col justify-center sm:py-12">
        <div className="p-10 xs:p-0 mx-auto md:w-full md:max-w-md">
          <div className="bg-dark border border-gray-600 shadow w-full rounded-lg divide-y divide-gray-200">
            <form className="px-5 py-7" onSubmit={handleSubmit}>
              <label className="font-semibold text-sm pb-1 block text-accent-content">
                User name
              </label>
              <input
                type="text"
                name="username"
                className="border rounded-lg px-3 py-2 mt-1 mb-5 text-sm w-full"
                value={formData.username}
                onChange={handleChange}
                required
              />
              <label className="font-semibold text-sm pb-1 block text-accent-content">
                Lastname
              </label>
              <input
                type="text"
                name="lastname"
                className="border rounded-lg px-3 py-2 mt-1 mb-5 text-sm w-full"
                value={formData.lastname}
                onChange={handleChange}
                required
              />
              <label className="font-semibold text-sm pb-1 block text-accent-content">
                E-mail
              </label>
              <input
                type="email"
                name="email"
                className="border rounded-lg px-3 py-2 mt-1 mb-5 text-sm w-full"
                value={formData.email}
                onChange={handleChange}
                required
              />
              <label className="font-semibold text-sm pb-1 block text-accent-content">
                Phone
              </label>
              <input
                type="tel"
                name="phone"
                className="border rounded-lg px-3 py-2 mt-1 mb-5 text-sm w-full"
                value={formData.phone}
                onChange={handleChange}
                required
              />
              <label className="font-semibold text-sm pb-1 block text-accent-content">
                Adress
              </label>
              <input
                type="text"
                name="address"
                className="border rounded-lg px-3 py-2 mt-1 mb-5 text-sm w-full"
                value={formData.address}
                onChange={handleChange}
                required
              />
              <label className="font-semibold text-sm pb-1 block text-accent-content">
                Password
              </label>
              <input
                type="password"
                name="password"
                className="border rounded-lg px-3 py-2 mt-1 mb-5 text-sm w-full"
                value={formData.password}
                onChange={handleChange}
                required
              />
              <label className="font-semibold text-sm pb-1 block text-accent-content">
                Repeat Password
              </label>
              <input
                type="password"
                name="confirmPassword"
                className="border rounded-lg px-3 py-2 mt-1 mb-5 text-sm w-full"
                value={formData.confirmPassword}
                onChange={handleChange}
                required
              />
              <button
                type="submit"
                className="transition duration-200 bg-blue-600 hover:bg-blue-500 focus:bg-blue-700 focus:shadow-sm focus:ring-4 focus:ring-blue-500 focus:ring-opacity-50 text-white w-full py-2.5 rounded-lg text-sm shadow-sm hover:shadow-md font-semibold text-center inline-block"
              >
                <span className="inline-block mr-2">Register</span>
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  stroke="currentColor"
                  className="w-4 h-4 inline-block"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth="2"
                    d="M17 8l4 4m0 0l-4 4m4-4H3"
                  />
                </svg>
              </button>
            </form>
          </div>
          <div className="py-5 text-center">
            <Link
              to="/login"
              className="btn btn-neutral text-white"
              onClick={() => window.scrollTo(0, 0)}
            >
              Already have an account? Please login.
            </Link>
          </div>
        </div>
      </div>
    </>
  );
};

export default Register;