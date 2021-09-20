import React from 'react'
import CountryPicker from './CountryPicker/CountryPicker'
import GainLose from './GainLose/GainLose'
import { IISList } from './IIS/IISList'
import { IISListAuth } from './IIS/IISListAuth'


export default function Body() {

    const sessionToken = sessionStorage.getItem("token");

    const localToken = localStorage.getItem("token");
    return (
        <div className='content'>
            <CountryPicker/>
            <GainLose />
            { (localToken!= null || sessionToken != null)? <IISListAuth/>:<IISList />}
        </div>
    )
}