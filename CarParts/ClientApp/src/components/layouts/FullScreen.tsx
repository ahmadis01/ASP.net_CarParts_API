import React, { PropsWithChildren } from 'react'
import Login from '../../pages/Login'
const FullScreen = (props: PropsWithChildren) => {
    return (
        <div>{props.children}</div>
    )
}

export default FullScreen