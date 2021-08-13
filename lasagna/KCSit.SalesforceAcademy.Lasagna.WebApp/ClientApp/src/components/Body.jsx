import React from 'react'
import GainLoseFetcher from './GainLose/GainLoseFetcher'


export default function Body() {
    return (
        <div className='content'>
            <GainLoseFetcher />
            <img src='../mockupbody.jpg' />
        </div>
    )
}