import React from 'react'
import { Menu } from 'semantic-ui-react'

export default function Pagination({count, page, handlePageClick}) {

    let totalCompanyIndex = eval(Math.ceil(count/10))

    const handleClick = (e) => {
        handlePageClick(e)
    }

     const ReturnPagenation = () => 
    {
        if(totalCompanyIndex == 0){
            return <Menu.Item onClick={handleClick} as='a'>{0}</Menu.Item>
        }
        else if(totalCompanyIndex <= 4){
            const companyArray = [];
            for (let numberLessFour = 0; numberLessFour < totalCompanyIndex; numberLessFour++) {
                companyArray.push(<Menu.Item onClick={handleClick} as='a'>{eval(page + numberLessFour)}</Menu.Item>)           
            }

            return (                           
                <>
                    {companyArray}
                </>)
        }
        else if(page < 3){
            return (                           
                <>
                    <Menu.Item onClick={handleClick} as='a'>{page}</Menu.Item>
                    <Menu.Item onClick={handleClick} as='a'>{eval(page + 1)}</Menu.Item>
                    <Menu.Item as='a'>...</Menu.Item>
                    <Menu.Item onClick={handleClick}as='a'>{totalCompanyIndex}</Menu.Item>
                </>)
        }
        else if(page >= totalCompanyIndex -3){
            return(
                <>
                <Menu.Item onClick={handleClick} as='a'>{eval(totalCompanyIndex -3)}</Menu.Item>
                <Menu.Item onClick={handleClick} as='a'>{eval(totalCompanyIndex -2)}</Menu.Item>
                <Menu.Item onClick={handleClick} as='a'>{eval(totalCompanyIndex -1)}</Menu.Item>
                <Menu.Item onClick={handleClick} as='a'>{eval(totalCompanyIndex)}</Menu.Item>
                </>
            )
        }
        else{
            return(
                <>
                <Menu.Item onClick={handleClick} as='a'>{1}</Menu.Item>
                <Menu.Item as='a'>...</Menu.Item>
                <Menu.Item onClick={handleClick} as='a'>{page}</Menu.Item>
                <Menu.Item onClick={handleClick} as='a'>{eval(page +1)}</Menu.Item>
                <Menu.Item as='a'>...</Menu.Item>
                <Menu.Item onClick={handleClick} as='a'>{totalCompanyIndex}</Menu.Item>
                </>
            )
        }
    }


    return (
            <ReturnPagenation />
    )
}
