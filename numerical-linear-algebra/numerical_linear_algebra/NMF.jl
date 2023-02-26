using LinearAlgebra

function nnmf(X::Array{Int64,2}, 
              r::Integer;
              maxiter::Integer=1000, 
              tol::Number=1e-4,
              V::Matrix{Float64}=rand(Float64, size(X, 1), r), 
        #matrica V tipa float64 , dimenzija X*1 i uzimamo r slučajno izgeneriranih brojeva tipa Float
              W::Matrix{Float64}=rand(Float64, r, size(X, 2))) 
    
    L = 0
    row, col = size(X)
    #kroz for petlju prolazimo onliko puta koliko smo postavili  maksimalan 
    #broj iteracija
    for iter in 1:maxiter
        #Prvi korak
        b = V * W
        #pogledamo umnozak matrica prije ulaska u petlju
        # Drugi korak
        V_new = copy(V)
        for k in 1:r
            for i in 1:row
                #uzimamo i-ti redak od X i pomoću apostrofa dobivamo adjungiranu matricu od itog retka
                #množimo je sa k-tim redkom matrice W i djelimo s (umnoškom
                #i-tog retka adjunktne matrice umnoška V i W->b i k-tog retka matrice W)
                weight = (X[i, :]' * W[k, :]) / (b[i, :]' * W[k, :])
                #izračunom dobivamo težinu koju pomnožimo s vrijednosti matrice na poziciji uređenog para (i,k)
                V_new[i, k] *= weight
            end
        end
        #Nakon što smo izvršili drugi korak ulazimo u treći gdje radimo s V_new i W, zapamtimo da smo
        #vrijednosti od V_new mijenjali u drugom koraku.
        # Treći korak
        b_new = V_new * W
        # Četvrti korak
        W_new = copy(W)
        for j in 1:col
            for k in 1:r
                #uzimamo adjungiranu matricu j-tog stupca matrice X i množimo sa k-tim stupcem nove matrice V_new
                #podjelimo sa adjunktnom matricom j-tog stupca matrice b_new i pomnožimo s k-tim stupcem matrice V_new
                weight = (X[:, j]' * V_new[:, k]) / (b_new[:, j]' * V_new[:, k])
                W_new[k, j] *= weight
            end
        end
        # ažuriramo vrijednosti V,W
        V, W = V_new, W_new;
        #računamo normu razlike prvobitne matrice X i umnoška njenog rastava
        L_new = norm(X - V * W)^2
        #gledamo razliku 
        rel_diff = abs(L_new - L) / (abs(L) + 1)
        if rel_diff <= tol
            break
        end
        # ažuriramo vrijednosti od L 
        L = L_new
        println("Iteration: $iter, Relative Difference: $rel_diff")
    end
    return V, W
end

a=[0 1
   2 3]

nnmf(a,3)

export nmf

function nmf(M::AbstractMatrix{T}, r::Integer;
             maxiter::Integer=100,
             W0::AbstractMatrix{T}=Matrix{T}(undef, 0, 0),
             H0::AbstractMatrix{T}=Matrix{T}(undef, 0, 0),
             updaterW!::Function=halsW!,
             updaterH!::Function=halsH!) where T <: Real

    #Konstante
    m, n = size(M)

    #Ako nam nisu dane matrice,generiramo ih slučajnim odabirom
    W0 = length(W0) == 0 ? rand(m, r) : W0
    W = copy(W0)
    H0 = length(H0) == 0 ? rand(r, n) : H0
    H = copy(H0)

    #Rezultirani vektor
    times = Array{Float64}(undef, maxiter + 1)
    times[1] = 0
    errors = Array{Float64}(undef, maxiter + 1)
    errors[1] = relreconstructionerror(M, W, H)

    #Alternativni optimizator
    for it in 1:maxiter
        time = @elapsed begin
            updaterW!(M, W, H)
            updaterH!(M, W, H)
        end
        times[it+1] = times[it] + time
        errors[it+1] = relreconstructionerror(M, W, H)
    end

    return W, H, times, errors
end


# Parameters
m = 16
n = 1
r = 8
k = 6

A = rand(m, r)
realx = rand_ksparse_vect(r, k)
b = A*realx

sols, breakpoints = homotopy(A, b)
@time sols, breakpoints = homotopy(A, b)

# Nice display
let matsols = zeros(r)
    for sol in sols
        matsols = hcat(sol, matsols)
    end
    println("\nSolution path")
    display(matsols)
end

println("\nReal x")
display(realx)

println("\nBreakpoints")
display(breakpoints)



