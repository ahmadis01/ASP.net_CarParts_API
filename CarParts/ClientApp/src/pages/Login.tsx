import React from 'react'
import TextField from '@mui/material/TextField'
import { useState } from 'react'
import { useLocation } from 'react-router-dom';
import { Button } from '@mui/material';



function Login() {
    const [loginForm, setForm] = useState({ username: '', password: "" });
    // const { pathname, key } = useLocation()
    // const { pathRoute, setPathRoute }: any = useStateContext(pathname);
    // console.log(pathname);
    return (
        <div className="bg-white dark:bg-gray-900">
            <div className="flex justify-center h-screen">
                <div className="hidden bg-cover lg:block lg:w-2/3" style={{ backgroundImage: 'url(https://images.unsplash.com/flagged/photo-1553505192-acca7d4509be?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxleHBsb3JlLWZlZWR8NHx8fGVufDB8fHx8&w=1000&q=80)' }}>
                    <div className="flex items-center h-full px-20 bg-gray-900 bg-opacity-40">
                        <div>
                            <h2 className="text-4xl font-bold text-white">Brand</h2>

                            <p className="max-w-xl mt-3 text-gray-300">
                                Lorem ipsum dolor sit, amet consectetur adipisicing elit. In
                                autem ipsa, nulla laboriosam dolores, repellendus perferendis libero suscipit nam temporibus
                                molestiae
                            </p>
                        </div>
                    </div>
                </div>

                <div className="flex items-center w-full max-w-md px-6 mx-auto lg:w-2/6">
                    <div className="flex-1">
                        <div className="text-center">
                            <h2 className="text-4xl font-bold text-center text-gray-700 dark:text-white">Brand</h2>

                            <p className="mt-3 text-gray-500 dark:text-gray-300">Sign in to access your account</p>
                        </div>

                        <div className="mt-8">
                            <form className="mt-6 flex justify-center items-center flex-col gap-5">
                                <TextField className='w-full' id="outlined-basic" label="Email" variant="outlined" />
                                <div className="w-full flex justify-end items-end flex-col">
                                    <a href="#" className="text-sm mb-2 text-gray-400 focus:text-blue-500 hover:text-blue-500 hover:underline">Forgot password?</a>
                                    <TextField className='w-full ' id="outlined-basic" label="Password" variant="outlined" />

                                </div>
                                <Button className='w-full' variant="contained">Sign in</Button>
                            </form>
                            <p className="mt-6 text-sm text-center text-gray-400">Don&#x27;t have an account yet? <a href="#" className="text-blue-500 focus:outline-none focus:underline hover:underline">Sign up</a>.</p>
                        </div>
                    </div>
                </div>
            </div>



        </div>
        // <div className='h-screen flex justify-center items-center'>
        //     <div className="w-full max-w-2xl pt-16  mx-auto overflow-hidden bg-white rounded-lg shadow-md dark:bg-gray-800">

        //         <div className="px-6 py-4">
        //             <h2 className="text-3xl font-bold text-center text-gray-700 dark:text-white">Brand</h2>

        //             <h3 className="mt-1 text-xl font-medium text-center text-gray-600 dark:text-gray-200">Welcome Back</h3>

        //             <p className="mt-1 text-center text-gray-500 dark:text-gray-400">Login or create account</p>

        //             <form>
        //                 <div className="w-full mt-4">


        //                     <TextField name='username' onChange={(e) => setForm((oldVal) => ({ ...oldVal, [e.target.name]: e.target.value }))} className="block w-full px-4 py-2 mt-2 text-gray-700 placeholder-gray-500 bg-white border rounded-md dark:bg-gray-800 dark:border-gray-600 dark:placeholder-gray-400 focus:border-blue-400 dark:focus:border-blue-300 focus:ring-opacity-40 focus:outline-none focus:ring focus:ring-blue-300" type="email" placeholder="Email Address" aria-label="Email Address" />




        //                 </div>

        //                 <div className="w-full mt-4">
        //                     <TextField name='password' onChange={(e) => setForm((oldVal) => ({ ...oldVal, [e.target.name]: e.target.value }))} className="block w-full px-4 py-2 mt-2 text-gray-700 placeholder-gray-500 bg-white border rounded-md dark:bg-gray-800 dark:border-gray-600 dark:placeholder-gray-400 focus:border-blue-400 dark:focus:border-blue-300 focus:ring-opacity-40 focus:outline-none focus:ring focus:ring-blue-300" type="password" placeholder="Password" aria-label="Password" />
        //                 </div>

        //                 <div className="flex items-center justify-between mt-4">
        //                     <a href="#" className="text-sm text-gray-600 dark:text-gray-200 hover:text-gray-500">Forget Password?</a>

        //                     <button className="px-4 py-2 leading-5 text-white transition-colors duration-300 transform bg-gray-700 rounded hover:bg-gray-600 focus:outline-none"
        //                         type="button"   >Login</button>
        //                 </div>
        //             </form>
        //         </div>

        //         <div className="flex items-center justify-center py-4 text-center bg-gray-50 dark:bg-gray-700">
        //             <span className="text-sm text-gray-600 dark:text-gray-200">Don't have an account? </span>

        //             <a href="#" className="mx-2 text-sm font-bold text-blue-500 dark:text-blue-400 hover:underline">Register</a>
        //         </div>
        //     </div>


        // </div>
    )
}

export default Login