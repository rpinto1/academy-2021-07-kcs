import React, {useState, useEffect} from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';
import { urlGetUser, headers } from '../UserManager';

export default function UserGreeting() {

    const [firstName, setFirstName] = useState('user');
    var creatingCode= '';

    useEffect(() => axios.get(urlGetUser, headers)
    .then(res => {
        const userInfo = res.data.result;
        setFirstName(userInfo.firstName);
    })
    .catch(error => console.log(error)),[firstName])
   
    //not real guid, just creates a buch of random chars so people cannot access the edit page directly format so people cannot access the page 
    function createCodeForuser(){
        for(var i = 0; i < 177 ; i++)
        creatingCode += Math.floor(Math.random() * 0xF).toString(0xF)
        return creatingCode;
    }

    const [url, setUrl] = useState("");

    useEffect(() => {
        createCodeForuser();
         setUrl('/user/profile/edit/' + creatingCode)
    }, []);
   

    return (
            <section className="greeting">
                <article className="avatar">
                    <img src="../blank-avatar-sm.jpg" alt="" />
                </article>

                <article>
                    <h1>Hello, {firstName}!</h1>
                    <Link to= {url} >Edit my profile</Link>
                </article>
            </section>
    )
}
