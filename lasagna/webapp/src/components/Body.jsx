import React from 'react'
import CountryPicker from './CountryPicker/CountryPicker'
import GainLose from './GainLose/GainLose'
import { IISList } from './IIS/IISList'
export default function Body() {


    return (
        <div className='content'>
            <CountryPicker/>
            <GainLose />
            <IISList />
        </div>
    )
}