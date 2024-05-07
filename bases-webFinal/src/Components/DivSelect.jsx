import React, { forwardRef, useEffect, useRef } from "react";

// Exporta un componente funcional utilizando forwardRef para admitir referencias
export default forwardRef(({ 
    // Props por defecto
    options = [], 
    icon = 'user',
    placeholder = '',
    name,
    id,
    value,
    classname,
    required,
    isFocused,
    handleChange
}, ref) => {

    // Utiliza useRef para crear una referencia si no se proporciona una ref
    const input = ref ? ref : useRef();

    // Utiliza useEffect para enfocar el input cuando isFocused es true
    useEffect(() => {
        if (isFocused) {
            input.current.focus();
        }
    }, []);

    // Retorna el JSX del componente
    return (
        <div className="input-group mb-3">
            <span className="input-group-text">
                {/* Renderiza un ícono basado en la clase proporcionada */}
                <i className={'fa-solid ' + icon}></i>
            </span>
            {/* Renderiza un select con las props proporcionadas */}
            <select 
                name={name}
                id={id} 
                value={value} 
                className={classname} 
                ref={input} // Utiliza la ref para acceder al elemento select
                required={required} 
                onChange={(e) => handleChange(e)} // Maneja el cambio del select
            >
                {/* Mapea las opciones y renderiza cada una como un option */}
                {options.map((op) => (
                    <option value={op.id} key={op.id}>{op.name}</option>
                ))}
            </select>
        </div>
    );
});
