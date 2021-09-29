import React, {useState, useEffect} from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import { urlGetUser, headers } from '../UserManager';

export default function UserGreeting() {

    const [firstName, setFirstName] = useState('User');
    

    useEffect(() => axios.get(urlGetUser, headers)
    .then(res => {
        const userInfo = res.data.result;
        setFirstName(userInfo.firstName);
    })
    .catch(error => console.log(error)),[firstName])
   

    return (
            <section className="greeting">
                <article className="avatar">
                    <img src="../blank-avatar-sm.jpg" alt="" />
                </article>

                <article>
                    <h1>Hello, {firstName}!</h1>
                    <Link to= '/user/profile/edit' >Edit my profile</Link>
                </article>
            </section>
    )
}
